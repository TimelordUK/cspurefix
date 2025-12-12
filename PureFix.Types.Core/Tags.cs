using System;
using System.Collections;

namespace PureFix.Types.Core
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
