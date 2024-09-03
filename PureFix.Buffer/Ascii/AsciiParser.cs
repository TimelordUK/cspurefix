using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public class AsciiParser
    {
        private static int nextId;
        byte _delimiter = AsciiChars.Soh;
        byte _writeDelimiter = AsciiChars.Pipe;
        int id = nextId++;
        private Memory<byte> _receivingBuffer = new(new byte[20 * 1024]);
        public void ParseFrom(Memory<byte> readFrom)
        {
            var state = this.state;
            var eq = AsciiChars.Eq;
            var zero = AsciiChars.Zero;
            var nine  = AsciiChars.Nine;
            var delimiter = _delimiter;
            var writeDelimiter = _writeDelimiter;
            var receivingBuffer = this._receivingBuffer;
            var switchDelimiter = writeDelimiter != delimiter;
            var readPtr = 0;
            var end = readFrom.Length;
            var readBuffer = readFrom.Span;
            while (readPtr < end)
            {
                var charAtPos = readBuffer[readPtr];
                var writePtr = receivingBuffer.saveChar(charAtPos) - 1
      switch (state.parseState)
                {
                    case ParseState.MsgComplete:
                        {
                            this.msg(writePtr)
                            continue;
                        }

                    case ParseState.BeginField:
                    {
                        var atDigit = charAtPos >= zero && charAtPos <= nine;
                        if (atDigit)
                        {
                            state.beginTag(writePtr);
                        }

                        break;
                    }

                    case ParseState.ParsingTag:
                    {
                        var isEquals = charAtPos == eq;
                      if (isEquals)
                      {
                          state.endTag(writePtr);
                      }
                      break;
                    }

                    case ParseState.ParsingRawData:
                        {
                            // keep skipping until length read, regardless of delimiter or not
                            if (state.incRaw())
                            {
                                // having consumed the raw field expecting delimiter
                                if (charAtPos == delimiter)
                                {
                                    if (switchDelimiter)
                                    {
                                        receivingBuffer.switchChar(writeDelimiter);
                                    }
                                    state.store();
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
                                    receivingBuffer.switchChar(writeDelimiter)
                                }
                                state.store()
                            }
                            break
                    }

                    default:
                    {
                        var st = state.parseState;
                        throw new InvalidDataException($"fix parser in unknown state {st}");
                    }
                }

                readPtr++;
            }

            switch (state.parseState)
            {
                case ParseState.MsgComplete:
                {
                    this.msg(receivingBuffer.getPos());
                        break;
                    }
            }
        }
    }
}
