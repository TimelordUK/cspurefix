using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Core;


namespace PureFix.Buffer.Ascii
{
    public partial class AsciiParser : IMessageParser
    {
        public byte Delimiter { get; set; } = AsciiChars.Soh;
        public byte WriteDelimiter { get; set; } = AsciiChars.Soh;
        public IFixDefinitions Definitions { get; }

        /// <summary>
        /// Optional session string store for header field interning.
        /// When set, CompID fields (49, 56, 50, 57) will be interned to reduce allocations.
        /// </summary>
        public ISessionStringStore? StringStore { get; set; }

        private readonly AsciiParseState _state;
        private readonly ISegmentParser _segmentParser;
        public Tags? Locations => _state.Locations;
        private readonly StoragePool _pool = new();
        private long _receivedBytes;
        private long _parsedMessages;
        private double _totalElapsedSegmentParseMicros;
        private double _totalElapsedParseMicros;
        public Stats ParserStats => new(_receivedBytes, _parsedMessages, _pool.Rents, _pool.Returns, _totalElapsedSegmentParseMicros, _totalElapsedParseMicros);

        public AsciiParser(IFixDefinitions definitions)
        {
            Definitions = definitions;
            _state = new AsciiParseState(definitions, _pool);
            _segmentParser = new AsciiSegmentParser(Definitions);
            _state.BeginMessage();
        }

        /// <summary>
        /// Creates a parser with a custom segment parser strategy.
        /// Use TagByTagSegmentParser for out-of-order tags (e.g., Bloomberg-style messages).
        /// </summary>
        public AsciiParser(IFixDefinitions definitions, ISegmentParser segmentParser)
        {
            Definitions = definitions;
            _state = new AsciiParseState(definitions, _pool);
            _segmentParser = segmentParser;
            _state.BeginMessage();
        }

        /// <summary>
        /// Creates a parser with the specified string store for header field interning.
        /// </summary>
        public AsciiParser(IFixDefinitions definitions, ISessionStringStore? stringStore)
            : this(definitions)
        {
            StringStore = stringStore;
        }

        /// <summary>
        /// Creates a parser with a custom segment parser and string store.
        /// </summary>
        public AsciiParser(IFixDefinitions definitions, ISegmentParser segmentParser, ISessionStringStore? stringStore)
            : this(definitions, segmentParser)
        {
            StringStore = stringStore;
        }

        // eventually need to parse the location set via segment parser to add all structures from the message.

        private void Msg(int ptr, Action<int, AsciiView>? onMsg, Action<StoragePool.Storage>? onDecode)
        {
            if (_state.Storage != null)
            {
                _state.Storage.Buffer.SetPos(ptr);
                onDecode?.Invoke(_state.Storage);
            }

            var startTicks = Stopwatch.GetTimestamp();
            var view = GetView(ptr);
            var elapsed = Stopwatch.GetElapsedTime(startTicks);
            _totalElapsedSegmentParseMicros += elapsed.TotalMicroseconds;

            if (view == null) return;
            _parsedMessages++;
            onMsg?.Invoke(ptr, view);
            _state.BeginMessage();
        }

        private AsciiView? GetView(int ptr)
        {
            if (_state.MsgType == null || Locations == null || _state.Buffer == null || _state.Storage == null)
            {
                return null;
            }

            // Rent view from pool with lazy structure parsing - structure will be parsed
            // on-demand when component/group access is needed (GetView, GetGroupInstance).
            // Simple field-by-tag access uses linear scan and doesn't need structure.
            return AsciiView.Rent(
                Definitions,
                _state.Storage,
                ptr,
                Delimiter,
                WriteDelimiter,
                _segmentParser,
                _state.MsgType,
                StringStore);  // Pass the parser's string store for header interning
        }

        /// <summary>
        /// Returns both the storage and view to their respective pools.
        /// </summary>
        public void ReturnView(AsciiView view)
        {
            _pool.Return(view.Storage);
            view.Return();
        }

        // will callback with ptr as to current location through byte array and the view with all parsed locations.
        public void ParseFrom(ReadOnlySpan<byte> readFrom, int end, Action<int, MsgView>? onView, Action<StoragePool.Storage>? onDecode = null)
        {
            const byte eq = AsciiChars.Eq;
            const byte zero = AsciiChars.Zero;
            const byte nine = AsciiChars.Nine;
            var delimiter = Delimiter;
            var switchDelimiter = WriteDelimiter != delimiter;
            var readPtr = 0;
            end = Math.Min(end, readFrom.Length);
            _receivedBytes += end;

            var startTicks = Stopwatch.GetTimestamp();

            if (_state.Buffer == null) return;

            try
            {
                while (readPtr < end)
                {
                    var charAtPos = readFrom[readPtr];
                    var writePtr = _state.Buffer.SaveChar(charAtPos) - 1;
                    switch (_state.ParseState)
                    {
                        case ParseState.MsgComplete:
                            {
                                Msg(writePtr, onView, onDecode);
                                continue;
                            }

                        case ParseState.BeginField:
                            {
                                var isDigit = charAtPos is >= zero and <= nine;
                                if (isDigit)
                                {
                                    _state.BeginTag(writePtr);
                                }
                                break;
                            }

                        case ParseState.ParsingTag:
                            {
                                var isEquals = charAtPos == eq;
                                if (isEquals)
                                {
                                    _state.EndTag(writePtr);
                                }
                                break;
                            }

                        case ParseState.ParsingRawData:
                            {
                                // keep skipping until length read, regardless of delimiter or not
                                if (_state.IncRaw())
                                {
                                    // having consumed the raw field expecting delimiter
                                    if (charAtPos == delimiter)
                                    {
                                        if (switchDelimiter)
                                        {
                                            _state.Buffer.SwitchChar(WriteDelimiter);
                                        }
                                        _state.Store();
                                    }
                                    else
                                    {
                                        throw new InvalidDataException(
                                            $"delimiter({delimiter}) expected at position {readPtr} when value is {charAtPos}");
                                    }
                                }
                                break;
                            }

                        case ParseState.ParsingRawDataLength:
                        case ParseState.ParsingValue:
                            {
                                if (charAtPos == delimiter)
                                {
                                    if (switchDelimiter)
                                    {
                                        _state.Buffer.SwitchChar(WriteDelimiter);
                                    }
                                    _state.Store();
                                }
                                break;
                            }

                        default:
                            {
                                var st = _state.ParseState;
                                throw new InvalidDataException($"fix parser in unknown state {st}");
                            }
                    }
                    readPtr++;
                }

                switch (_state.ParseState)
                {
                    case ParseState.MsgComplete:
                        {
                            Msg(_state.Buffer.GetPos(), onView, onDecode);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                // return buffer given this message has failed to deliver
                if (_state.Storage != null) _pool.Return(_state.Storage);
                throw;
            }
            finally
            {
                var elapsedTime = Stopwatch.GetElapsedTime(startTicks);
                _totalElapsedParseMicros += elapsedTime.TotalMicroseconds;
                FixMetrics.ParseLatency.Record(elapsedTime.TotalMicroseconds,
                    new KeyValuePair<string, object?>("bytes", end));
            }
        }

        public void Return(StoragePool.Storage sto)
        {
            _pool.Return(sto);
        }
    }
}
