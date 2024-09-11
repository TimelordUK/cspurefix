using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PureFix.Buffer.Segment
{
    public enum SegmentType
    {
        Component = 0,
        Group = 1,
        Msg = 2,
        Gap = 3,
        Batch = 4,
        Unknown = 5
    }
}
