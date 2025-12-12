using PureFix.Types;
using PureFix.Types.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public partial class AsciiSegmentParser
    {
        // the internal state should a fatal error be encountered.
        public record Summary(
            string MsgType,
            TagPos[] Tags,
            int Last,
            string MsgDefinition,
            int CurrentTagPosition,
            string Peek,
            string[] Segments,
            string[] StructureStack);
    }
}
