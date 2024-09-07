using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public class AsciiView(
        FixDefinitions definitions,
        SegmentDescription segment,
        ElasticBuffer buffer,
        Structure? structure,
        int ptr,
        int delimiter,
        int writeDelimiter)
        : MsgView(definitions, segment, structure)
    {
        public int Ptr { get; } = ptr;
        public int Delimiter { get; } = delimiter;
        int WriteDelimiter { get; } = writeDelimiter;
        public ElasticBuffer Buffer { get; } = buffer;
    }
}
