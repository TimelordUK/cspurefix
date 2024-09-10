using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;
using PureFix.Tag;

namespace PureFix.Buffer.Ascii
{
    internal class AsciiParseState(FixDefinitions definitions)
    {
        private MessageDefinition? _message;
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
        private ElasticBuffer? _buffer;
        private Tags? _locations;
        public Tags? Locations => _locations;
        public ElasticBuffer? Buffer => _buffer;

        public string? MsgType { get; private set; }

        public void BeginTag(int pos)
        {
            ParseState = ParseState.ParsingTag;
            _tagStartPos = pos;
            _equalPos = _valueEndPos = -1;
            _currentTag = 0;
        }

        public void BeginMessage()
        {
            _buffer = new ElasticBuffer(1 * 1024);
            _locations = new Tags(100);
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
            MsgType = null;
            _message = null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.Append($"ParseState = {ParseState} ");
            sb.Append($"count = {_count} ");
            sb.Append($"currentTag = {_currentTag} ");
            sb.Append($"tagStartPos = {_tagStartPos} ");
            sb.Append($"valueEndPos = {_valueEndPos} ");
            sb.Append($"equalPos = {_equalPos} ");
            sb.Append($"rawDataRead = {_rawDataRead} ");
            sb.Append($"rawDataLen = {_rawDataLen} ");
            sb.Append($"bodyLen = {_bodyLen} ");
            sb.Append($"msgType = {MsgType} ");
            sb.Append($"buffer = {_buffer} ");
            sb.Append($"message = {_message} ");

            return sb.ToString();
        }

        public void EndTag(int pos)
        {
            if (_buffer == null) return;
            _equalPos = pos;
            switch (ParseState)
            {
                case ParseState.ParsingTag:
                {
                    _currentTag = (int)_buffer.GetWholeNumber(_tagStartPos, pos - 1);
                    break;
                }

                default:
                    throw new InvalidDataException($"EndTag: unexpected state {ParseState}");
            }
            // if a raw tag, then need length to skip that many bytes
            CheckRawTag();
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
            if (msg.ContainedLength.ContainsKey(_currentTag))
            {
                ParseState = ParseState.ParsingRawDataLength;
            }
            else
            {
                _rawDataRead = 0;
                var isData = _rawDataLen > 0;
                ParseState = isData ? ParseState.ParsingRawData : ParseState.ParsingValue;
            }
        }

        public void Store()
        {
            if (_buffer == null) return;
            if (_locations == null) return;
            var valueEndPos = _buffer.GetPos() - 1;
            _valueEndPos = valueEndPos;
            var equalPos = _equalPos;
            var tag = _currentTag;
            
            switch (ParseState)
            {
                case ParseState.ParsingValue:
                case ParseState.ParsingRawData:
                {
                    _rawDataLen = 0;
                    _locations.Store(equalPos + 1, valueEndPos - equalPos - 1, tag);
                    break;
                }

                case ParseState.ParsingRawDataLength:
                {
                    _rawDataLen = (int)_buffer.GetWholeNumber(equalPos + 1, valueEndPos - 1);
                    _locations.Store(equalPos + 1, valueEndPos - equalPos - 1, tag);
                    break;
                }
            }

            ParseState = ParseState.BeginField;
            _count++;
            var nextTagPos = _locations.NextTagPos;

            switch (tag)
            {
                case Tags.BeginString:
                {
                    if (nextTagPos != 1)
                    {
                        throw new InvalidDataException($"BeginString: not expected at position [{nextTagPos}]");
                    }

                    break;
                }

                case Tags.BodyLengthTag:
                {
                    if (nextTagPos != 2)
                    {
                        throw new InvalidDataException($"BodyLengthTag: not expected at position [{nextTagPos}]");
                    }

                    _bodyLen = (int)_buffer.GetWholeNumber(equalPos + 1, valueEndPos - 1);
                    _checksumExpectedPos = _bodyLen + valueEndPos;
                    break;
                }

                case Tags.MessageTag:
                {
                    if (nextTagPos != 3)
                    {
                        throw new InvalidDataException($"MsgTag: not expected at position [{nextTagPos}]");
                    }

                    MsgType = _buffer.GetString(equalPos + 1, valueEndPos);
                    if (definitions.Message.TryGetValue(MsgType, out var message))
                    {
                        _message = message;
                    }
                    else
                    {
                        throw new InvalidDataException($"MsgType: [{MsgType}] not in definitions.");
                    }

                    break;
                }

                case Tags.CheckSumTag:
                {
                    if (valueEndPos < _bodyLen)
                    {
                        throw new InvalidDataException($"CheckSumTag: [{valueEndPos}] expected after {_bodyLen}");
                    }

                    ParseState = ParseState.MsgComplete;
                    break;
                }

                default:
                {
                    if (_checksumExpectedPos > 0 && valueEndPos > _checksumExpectedPos)
                    {
                        throw new InvalidDataException($"Tag: [{tag}] cant be after {_checksumExpectedPos}");
                    }

                    break;
                }
            }

            switch (nextTagPos)
            {
                case 1:
                {
                    if (tag != Tags.BeginString)
                    {
                        throw new InvalidDataException($"position 1 [{tag}] must be BeginString: 8=");
                    }

                    break;
                }
                case 2:
                {
                    if (tag != Tags.BodyLengthTag)
                    {
                        throw new InvalidDataException($"position 2 [{tag}] must be BodyLengthTag: 9=");
                    }

                    break;
                }
                case 3:
                {
                    if (tag != Tags.MessageTag)
                    {
                        throw new InvalidDataException($"position 3 [{tag}] must be MsgTag: 35=");
                    }
                }
                    break;
            }
        }
        
        public bool IncRaw()
        {
            ++_rawDataRead;
            return _rawDataRead == _rawDataLen + 1;
        }
    }
}
