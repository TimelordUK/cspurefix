using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Definition;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PureFix.Buffer.Ascii
{
    internal class AsciiParseState
    {
        private MessageDefinition _message;
        private Tags _locations;
        public ParseState ParseState { get; private set; }

        private int _bodyLen;
        private int _checksumExpectedPos;
        private int _tagStartPos;
        private int _equalPos;
        private int _valueEndPos;
        private int _count;
        private int _currentTag;
        private int _rawDataLen;
        private int _rawDataRead;
        private FixDefinitions _definitions;
        private string? _msgType;
        private ElasticBuffer _elasticBuffer;

        public void BeginTag(int pos)
        {
            ParseState = ParseState.ParsingTag;
            _tagStartPos = pos;
            _equalPos = _valueEndPos = -1;
            _currentTag = 0;
        }

        public void BeginMessage()
        {
            _elasticBuffer.Reset();
            _locations.Reset();
            _checksumExpectedPos = 0;
            ParseState = ParseState.BeginField;
            _count = 0;
            _currentTag = 0;
            _tagStartPos = 0;
            _valueEndPos = 0;
            _equalPos = 0;
            _rawDataRead = 0;
            _rawDataLen = 0;
            _bodyLen = 0;
            _msgType = null;
        }

        public void EndTag(int pos)
        {
            _equalPos = pos;
            switch (ParseState)
            {
                case ParseState.ParsingTag:
                {
                    _currentTag = _elasticBuffer.GetWholeNumber(_tagStartPos, pos - 1);
                    break;
                }

                default:
                    throw new InvalidDataException($"EndTag: unexpected state {ParseState}");
            }
            // if a raw tag, then need length to skip that many bytes
            this.CheckRawTag();
        }

        private void CheckRawTag()
        {
            var msg = _message;
            if (!(msg?.ContainsRaw ?? false))
            {
                // optimisation as will never hit raw data
                ParseState = ParseState.ParsingValue;
                return;
            }
            // if this is a raw data tag then need to keep track of the length
            // on this field to skip that many bytes.
            var isDataLength = msg.ContainedLength[_currentTag];
            if (isDataLength)
            {
                ParseState = ParseState.ParsingRawDataLength;
            }
            else
            {
                _rawDataRead = 0;
                var isData = _rawDataLen > 0;
                if (isData)
                {
                    ParseState = ParseState.ParsingRawData;
                }
                else
                {
                    ParseState = ParseState.ParsingValue;
                }
            }
        }

        public void Store()
        {
            var valueEndPos = _elasticBuffer.GetPos() - 1;
            _valueEndPos = valueEndPos;
            var equalPos = _equalPos;
            var tag = _currentTag;
            var locations = _locations;
            var buffer = _elasticBuffer;
            var _terminates = _checksumExpectedPos;

            switch (ParseState)
            {
                case ParseState.ParsingValue:
                case ParseState.ParsingRawData:
                {
                    _rawDataLen = 0;
                    locations.Store(equalPos + 1, valueEndPos - equalPos - 1, tag);
                    break;
                }

                case ParseState.ParsingRawDataLength:
                {
                    _rawDataLen = buffer.GetWholeNumber(equalPos + 1, valueEndPos - 1);
                    locations.Store(equalPos + 1, valueEndPos - equalPos - 1, tag);
                    break;
                }
            }

            ParseState = ParseState.BeginField;
            _count++;
            var nextTagPos = locations.NextTagPos;

            switch (tag)
            {
                case Tags.BeginString:
                    {
                        if (nextTagPos != 1)
                        {
                            throw new InvalidDataException($"BeginString: not expected at position[{nextTagPos}]");
                        }
                        break;
                    }

                case Tags.BodyLengthTag:
                    {
                        if (nextTagPos !== 2)
                        {
                            throw new Error(`BodyLengthTag: not expected at position[${ nextTagPos }]`)
        }
                        this.bodyLen = buffer.getWholeNumber(equalPos + 1, valueEndPos - 1)
                  this.checksumExpectedPos = this.bodyLen + valueEndPos
                  this.elasticBuffer.checkGrowBuffer(this.bodyLen)
                  break
                }

                case Tags.MsgTag:
                    {
                        if (nextTagPos !== 3)
                        {
                            throw new Error(`MsgTag: not expected at position[${ nextTagPos }]`)
        }
                        this.msgType = buffer.getString(equalPos + 1, valueEndPos)
                  this.message = this.definitions.message.get(this.msgType)
                  break
                }

                case Tags.CheckSumTag:
                    {
                        if (valueEndPos < this.bodyLen)
                        {
                            throw new Error(`CheckSumTag: [${ valueEndPos }] expected after ${ this.bodyLen}`)
        }
                        this.parseState = ParseState.MsgComplete
                  break
                }

                default:
                    {
                        if (terminates && valueEndPos > terminates)
                        {
                            throw new Error(`Tag: [${ tag }] cant be after ${ terminates}`)
        }
                        break
                }
            }

            switch (nextTagPos)
            {
                case 1:
                    {
                        if (tag !== Tags.BeginString)
                        {
                            throw new Error(`position 1[${ tag }] must be BeginString: 8 =`)
        }
                        break
                }
                case 2:
                    {
                        if (tag !== Tags.BodyLengthTag)
                        {
                            throw new Error(`position 2[${ tag }] must be BodyLengthTag: 9 =`)
        }
                        break
                }
                case 3:
                    {
                        if (tag !== Tags.MsgTag)
                        {
                            throw new Error(`position 3[${ tag }] must be MsgTag: 35 =`)
        }
                        break
                }
            }
        }

        public bool IncRaw()
        {
            ++_rawDataRead;
            return _rawDataRead == _rawDataLen + 1;
        }
    }
}
