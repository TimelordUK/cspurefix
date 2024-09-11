using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Segment
{
    public record SegmentSummary(string Name, int Depth, int StartTag, int StartPosition, int EndTag, int EndPosition, int? DelimiterTag, int[] DelimterPositions)
    {
        public static SegmentSummary FromDescription(SegmentDescription d)
        {
            return new SegmentSummary(d.Set?.Name ?? "na", d.Depth, d.StartTag, d.StartPosition,
                d.EndTag, d.EndPosition, d.DelimiterTag, d.DelimiterPositions.ToArray());
        }
}
}
