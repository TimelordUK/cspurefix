namespace PureFix.Tag
{
    public readonly record struct TagPos(int Position, int Tag, int Start, int Len)
    {
        public int End => Start + Len - 1;

        public static int BinarySearch(IReadOnlyList<TagPos> ar, int tag)
        {
            var m = 0;
            var n = ar.Count - 1;
            while (m <= n)
            {
                var k = (n + m) >> 1;
                var cmp = tag - (ar[k].Tag);
                switch (cmp)
                {
                    case > 0:
                        m = k + 1;
                        break;
                    case < 0:
                        n = k - 1;
                        break;
                    default:
                        return k;
                }
            }

            return -m - 1;
        }

        public static int Compare(TagPos lhs , TagPos rhs) {
            if (lhs.Tag < rhs.Tag)
            {
                return -1;
            }
            if (lhs.Tag > rhs.Tag)
            {
                return 1;
            }
            if (lhs.Tag == rhs.Tag && lhs.Start == rhs.Start)
            {
                return 0;
            }

            return lhs.Start < rhs.Start ? -1 : lhs.Start > rhs.Start ? 1 : 0;
        }
    }
}
