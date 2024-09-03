using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PureFix.Buffer.Ascii
{
    public class AsciiParser
    {
        private static int _nextId;
        byte _delimiter = AsciiChars.Soh;
        byte _writeDelimiter = AsciiChars.Pipe;
        int id = _nextId++;
        private Memory<byte> _receivingBuffer = new(new byte[20 * 1024]);
        private AsciiParseState _state = new ();

        private void Msg(int ptr)  {
            _state.BeginMessage();
        }

    public void ParseFrom(Memory<byte> readFrom)
        {
            var state = this._state;
            var eq = AsciiChars.Eq;
            var zero = AsciiChars.Zero;
            var nine = AsciiChars.Nine;
            var delimiter = _delimiter;
            var writeDelimiter = _writeDelimiter;
            var receivingBuffer = new ElasticBuffer();
            var switchDelimiter = writeDelimiter != delimiter;
            var readPtr = 0;
            var end = readFrom.Length;
            var readBuffer = readFrom.Span;
            while (readPtr < end)
            {
                var charAtPos = readBuffer[readPtr];
                var writePtr = receivingBuffer.SaveChar(charAtPos) - 1;
                switch (state.ParseState)
                {
                    case ParseState.MsgComplete:
                    {
                        Msg(writePtr);
                        continue;
                    }

                    case ParseState.BeginField:
                    {
                        var atDigit = charAtPos >= zero && charAtPos <= nine;
                        if (atDigit)
                        {
                            state.BeginTag(writePtr);
                        }

                        break;
                    }

                    case ParseState.ParsingTag:
                    {
                        var isEquals = charAtPos == eq;
                        if (isEquals)
                        {
                            state.EndTag(writePtr);
                        }

                        break;
                    }

                    case ParseState.ParsingRawData:
                    {
                        // keep skipping until length read, regardless of delimiter or not
                        if (state.IncRaw())
                        {
                            // having consumed the raw field expecting delimiter
                            if (charAtPos == delimiter)
                            {
                                if (switchDelimiter)
                                {
                                    receivingBuffer.SwitchChar(writeDelimiter);
                                }

                                state.Store();
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
                                receivingBuffer.SwitchChar(writeDelimiter);
                            }

                            state.Store();
                        }

                        break;
                    }

                    default:
                    {
                        var st = state.ParseState;
                        throw new InvalidDataException($"fix parser in unknown state {st}");
                    }
                }

                readPtr++;
            }

            switch (state.ParseState)
            {
                case ParseState.MsgComplete:
                {
                    msg(receivingBuffer.GetPos());
                    break;
                }
            }
        }
    }
}
