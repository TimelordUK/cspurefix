using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public static class Extensions
    {
        /// <summary>
        /// Treats the source bytes as ascii characters and populates the destination span
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyByteSpanToCharSpan(this scoped ReadOnlySpan<byte> source, scoped Span<char> destination)
        {
            var length = source.Length;

            for (var i = 0; i < length; i++)
            {
                destination[i] = (char)source[i];
            }
        }
    }
}
