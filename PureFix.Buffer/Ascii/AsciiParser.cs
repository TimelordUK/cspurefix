using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Tag;
using Microsoft.Extensions.ObjectPool;

namespace PureFix.Buffer.Ascii
{
    public class AsciiParser
    {
        public readonly record struct Stats(long ReceivedBytes, long ParsedMessages, long Rents, long Returns);
        public class Pool
        {
            public class Storage
            {
                public Storage()
                {
                    Buffer = new ElasticBuffer();
                    Locations = new Tags();
                }
                public ElasticBuffer Buffer { get; }
                public Tags Locations { get; }
                public void Reset()
                {
                    Buffer.Reset();
                    Locations.Reset();
                }
                public override string ToString()
                {
                    return $"[{Buffer.Pos}, {Locations.NextTagPos}]";
                }
            }
            public Storage Rent()
            {
                var instance = _pool.Get();
                instance.Reset();
                ++_rents;
                return instance;
            }
            public void Deliver(Storage storage)
            {
                _delivered.Enqueue(storage);
            }
            public void Reclaim()
            {
                while (_delivered.Count > 0)
                {
                    _returns++;
                    var instance = _delivered.Dequeue();
                    _pool.Return(instance);
                }
            }
            public long Imbalance => _rents - _returns;
            public long Rents => _rents;
            public long Returns => _returns;

            private long _rents;
            private long _returns;
            private readonly ObjectPool<Storage> _pool = new DefaultObjectPool<Storage>(new DefaultPooledObjectPolicy<Storage>());
            private readonly Queue<Storage> _delivered = [];
        }
        private static int _nextId;
        public byte Delimiter { get; set; } = AsciiChars.Soh;
        public byte WriteDelimiter { get; set; } = AsciiChars.Pipe;
        public FixDefinitions Definitons { get; }
        public int ID { get; } = Interlocked.Increment(ref _nextId);

        private readonly AsciiParseState _state;
        private readonly AsciiSegmentParser _segmentParser;
        public Tags? Locations => _state.Locations;
        private readonly Pool _pool = new();
        private long _receivedBytes;
        private long _parsedMessages;
        public Stats ParserStats => new(_receivedBytes, _parsedMessages, _pool.Rents, _pool.Returns);

        public AsciiParser(FixDefinitions definitions)
        {
            Definitons = definitions;
            _state = new AsciiParseState(definitions, _pool);
            _segmentParser = new AsciiSegmentParser(Definitons);
            _state.BeginMessage();
        }

        // eventually need to parse the location set via segment parser to add all structures from the message.

        private void Msg(int ptr, Action<int, AsciiView>? onMsg)
        {
            var view = GetView(ptr);
            if (view == null) return;
            _parsedMessages++;
            onMsg?.Invoke(ptr, view);
            // storage for this message will be re-used on next invocation now its been handed to
            // application
            if (_state.Storage != null) _pool.Deliver(_state.Storage);
            _state.BeginMessage();
        }

        private AsciiView? GetView(int ptr)
        {
            if (_state.MsgType == null || Locations == null || _state.Buffer == null)
            {
                return null;
            }
            var structure = _segmentParser.Parse(_state.MsgType, Locations, Locations.NextTagPos - 1);
            var msgSegment = structure?.Msg();
            if (msgSegment != null)
            {
                var view = new AsciiView(Definitons, msgSegment, _state.Buffer, structure, ptr, Delimiter, WriteDelimiter);
                return view;
            }

            structure = new Structure(Locations, []);
            var segment = new SegmentDescription("unknown", Locations[0].Tag, null, 0, 1, SegmentType.Unknown)
                {
                    EndPosition = Locations.NextTagPos - 1
                };
            return new AsciiView(Definitons, segment, _state.Buffer, structure, ptr, Delimiter, WriteDelimiter);
        }

        // will callback with ptr as to current location through byte array and the view with all parsed locations.
        public void ParseFrom(Span<byte> readFrom, Action<int, AsciiView>? onView)
        {
            const byte eq = AsciiChars.Eq;
            const byte zero = AsciiChars.Zero;
            const byte nine = AsciiChars.Nine;
            var delimiter = Delimiter;
            var switchDelimiter = WriteDelimiter != delimiter;
            var readPtr = 0;
            var end = readFrom.Length;
            var readBuffer = readFrom;
            _receivedBytes += readFrom.Length;

            if (_state.Buffer == null) return;
            try
            {
                while (readPtr < end)
                {
                    var charAtPos = readBuffer[readPtr];
                    var writePtr = _state.Buffer.SaveChar(charAtPos) - 1;
                    switch (_state.ParseState)
                    {
                        case ParseState.MsgComplete:
                        {
                            Msg(writePtr, onView);
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
                        Msg(_state.Buffer.GetPos(), onView);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // return buffer given this message has failed to deliver
                if (_state.Storage != null) _pool.Deliver(_state.Storage);
                throw;
            } finally
            {
                // any views dispatched on the callback from previous call are considered ready to be reclaimed, the expectation
                // being the recipient must have extracted required data or parsed into a concrete type within the callbak itself.s
                _pool.Reclaim();
            }
        }
    }
}
