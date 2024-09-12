using System;
using System.Collections;
using PureFix.Types;

namespace PureFix.Tag
{
    public class Tags
    {
        public const int BeginString = (int)MsgTag.BeginString;
        public const int BodyLengthTag = (int)MsgTag.BodyLength;
        public const int CheckSumTag = (int)MsgTag.CheckSum;
        public const int MessageTag = (int)MsgTag.MsgType;

        private TagPos[] _tagPos;
        private int _ptr;

        public Tags() : this(50)
        {
        }

        public Tags(int startingCapacity)
        {
            _tagPos = new TagPos[startingCapacity];
        }

        public override string ToString()
        {
            return $"[{NextTagPos}] {string.Join(", ", _tagPos[..NextTagPos].Select(tp=>tp.ToString()))}";
        }

        public TagPos[] ToArray()
        {
            var array = new TagPos[_ptr];
            Array.Copy(_tagPos, array, _ptr);

            return array;
        }

        public int NextTagPos => _ptr;

        public Tags(Tags that) : this()
        {
            _tagPos = that._tagPos[..that._ptr];
            _ptr = that._ptr;
        }

        public int Count => _ptr;

        public TagPos[] Slice(int start, int end)
        {
            return _tagPos[start..end];
        }

        public TagPos this[int x] => _tagPos[x];
        public TagPos this[Index i] => _tagPos[i];

        // used by the compiler to produce types representing the dictionary.
        public static string ToCsType(TagType tagType)
        {
            switch (tagType)
            {
                case TagType.String:
                    return "string";

                case TagType.Length:
                case TagType.Int:
                    return "int";

                case TagType.Float:
                    return "double";

                case TagType.RawData:
                    return "byte[]";

                case TagType.Boolean:
                    return "bool";

                case TagType.UtcTimestamp:
                case TagType.UtcDateOnly:
                case TagType.UtcTimeOnly:
                case TagType.LocalDate:
                    return "DateTime";

                default:
                    return "string";
            }
        }


        public Tags Clone()
        {
            return new Tags(this);
        }

        public void Reset()
        {
            _ptr = 0;
        }

        public void Store(int start, int len, int tag)
        {
            if (_ptr == _tagPos.Length)
            {
                var grown = new TagPos[_tagPos.Length * 2];
                Array.Copy(_tagPos, grown, _tagPos.Length);
                _tagPos = grown;
            }
            _tagPos[_ptr] = new TagPos(_ptr++, tag, start, len);
        }
    }
}
