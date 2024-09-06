using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Types.tag;

namespace PureFix.Buffer.Ascii
{
    public class MsgView(FixDefinitions definitions, SegmentDescription segment , Structure? structure)
    {
        public FixDefinitions Definitions { get { return definitions; } }
        public SegmentDescription Segment { get { return segment; } }
        public Structure? Structure { get { return structure; } }
        public Tags? Tags => Structure?.Tags;
    }
}
