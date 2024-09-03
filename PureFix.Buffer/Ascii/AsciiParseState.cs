using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;

namespace PureFix.Buffer.Ascii
{
    internal class AsciiParseState
    {
        private MessageDefinition _message;
        private Tags locations;
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
        private string _msgType;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool IncRaw()
        {
            ++_rawDataRead;
            return _rawDataRead == _rawDataLen + 1;
        }
    }
}
