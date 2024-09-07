using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PureFix.Dictionary.Definition;
using PureFix.Transport;
using PureFix.Types.tag;

namespace PureFix.Buffer.Ascii
{
    public class AsciiParser
    {
        private static int _nextId;
        public byte Delimiter { get; set; } = AsciiChars.Soh;
        public byte WriteDelimiter { get; set; } = AsciiChars.Pipe;
        private readonly FixDefinitions _definitions;
        public FixDefinitions Definitons => _definitions;
        public Tags Locations { get; } = new ();

        private readonly ElasticBuffer _receivingBuffer;
        public ElasticBuffer ReceivingBuffer => _receivingBuffer;
        private readonly FixDuplex<MsgView> _txDuplex;

        int id = _nextId++;
        private readonly AsciiParseState _state;
        private readonly AsciiSegmentParser _segmentParser;
        
        public AsciiParser(FixDefinitions definitions, FixDuplex<MsgView> txDuplex, ElasticBuffer? receivingBuffer)
        {
            _definitions = definitions;
            // publish completed parsed views on tx channel.
            _txDuplex = txDuplex;
            _receivingBuffer = receivingBuffer ?? new ElasticBuffer();
            _state = new AsciiParseState(_receivingBuffer, definitions, Locations);
            _segmentParser = new AsciiSegmentParser(_definitions);
        }

        // eventually need to parse the location set via segment parser to add all structures from the message.
        private void Msg(int ptr)
        {
            var view = GetView(ptr);
            if (view == null) return;
            _txDuplex.Writer.WriteAsync(view);
            _state.BeginMessage();
        }

        private AsciiView? GetView(int ptr)
        {
            if (_state.MsgType == null)
            {
                return null;
            }
            var structure = _segmentParser.Parse(_state.MsgType, Locations.Clone(), Locations.NextTagPos - 1);
            var msg = structure?.Msg();
            if (msg == null)
            {
                return null;
            }
            var view = new AsciiView(_definitions, msg, _receivingBuffer.Clone(), structure, ptr, Delimiter, WriteDelimiter);
            return view;
        }

        public void ParseFrom(Memory<byte> readFrom)
        {
            const byte eq = AsciiChars.Eq;
            const byte zero = AsciiChars.Zero;
            const byte nine = AsciiChars.Nine;
            var delimiter = Delimiter;
            var switchDelimiter = WriteDelimiter != delimiter;
            var readPtr = 0;
            var end = readFrom.Length;
            var readBuffer = readFrom.Span;
            if (_receivingBuffer.Pos == 0)
            {
                _state.BeginMessage();
            }
            while (readPtr < end)
            {
                var charAtPos = readBuffer[readPtr];
                var writePtr = _receivingBuffer.SaveChar(charAtPos) - 1;
                switch (_state.ParseState)
                {
                    case ParseState.MsgComplete:
                    {
                        Msg(writePtr);
                        continue;
                    }

                    case ParseState.BeginField:
                    {
                        var atDigit = charAtPos is >= zero and <= nine;
                        if (atDigit)
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
                                    _receivingBuffer.SwitchChar(WriteDelimiter);
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
                                _receivingBuffer.SwitchChar(WriteDelimiter);
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
                    Msg(_receivingBuffer.GetPos());
                    break;
                }
            }
        }
    }
}
