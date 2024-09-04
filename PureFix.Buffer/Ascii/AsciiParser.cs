using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PureFix.Dictionary.Definition;
using PureFix.Types.tag;

namespace PureFix.Buffer.Ascii
{
    public class AsciiParser(FixDefinitions definitions, ElasticBuffer receivingBuffer)
    {
        private static int _nextId;
        byte _delimiter = AsciiChars.Soh;
        byte _writeDelimiter = AsciiChars.Pipe;
        int id = _nextId++;
        private readonly AsciiParseState _state = new(receivingBuffer, definitions, new Tags());

        private void Msg(int ptr)
        {
            _state.BeginMessage();
        }

        public void ParseFrom(Memory<byte> readFrom)
        {
            const byte eq = AsciiChars.Eq;
            const byte zero = AsciiChars.Zero;
            const byte nine = AsciiChars.Nine;
            var delimiter = _delimiter;
            var switchDelimiter = _writeDelimiter != delimiter;
            var readPtr = 0;
            var end = readFrom.Length;
            var readBuffer = readFrom.Span;
            while (readPtr < end)
            {
                var charAtPos = readBuffer[readPtr];
                var writePtr = receivingBuffer.SaveChar(charAtPos) - 1;
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
                                    receivingBuffer.SwitchChar(_writeDelimiter);
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
                                receivingBuffer.SwitchChar(_writeDelimiter);
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
                    Msg(receivingBuffer.GetPos());
                    break;
                }
            }
        }
    }
}
