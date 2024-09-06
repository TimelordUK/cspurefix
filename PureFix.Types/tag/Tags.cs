using System.Collections;

namespace PureFix.Types.tag
{
    public class Tags(int startingCapacity = 30 * 000) : IEnumerable<TagPos>
    {
        public const int BeginString = (int)MsgTag.BeginString;
        public const int BodyLengthTag = (int)MsgTag.BodyLength;
        public const int CheckSumTag = (int)MsgTag.CheckSum;
        public const int MessageTag = (int)MsgTag.MsgType;

        private readonly List<TagPos> _tagPos = new(startingCapacity);

        public override string ToString()
        {
            return $"[{_tagPos.Count}] {string.Join(", ", _tagPos)}";
        }

        public int NextTagPos => _tagPos.Count;

        public Tags(Tags that) : this()
        {
            _tagPos = [..that._tagPos];
        }

        public TagPos this[int x] => _tagPos[x];

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
                    return "Memory<byte>";

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
            _tagPos.Clear();
        }

        public void Store(int start, int len, int tag)
        {
            _tagPos.Add(new TagPos(_tagPos.Count, tag, start, len));
        }

        public IEnumerator<TagPos> GetEnumerator()
        {
            return _tagPos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
