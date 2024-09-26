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
        public static void CopyByteSpanToCharSpan(this ReadOnlySpan<byte> source, Span<char> destination)
        {
            var length = source.Length;

            for (var i = 0; i < length; i++)
            {
                destination[i] = (char)source[i];
            }
        }

        public static void MergeFrom(this IStandardHeader header, IStandardHeader rhs)
        {
            if (rhs.SendingTime != null) header.OrigSendingTime = rhs.SendingTime;
            if (rhs.MsgSeqNum != null) header.MsgSeqNum = rhs.MsgSeqNum;
            if (rhs.PossDupFlag != null) header.PossDupFlag = rhs.PossDupFlag;
        }
    }
}
