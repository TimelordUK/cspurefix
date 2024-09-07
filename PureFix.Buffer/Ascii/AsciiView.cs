using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public class AsciiView : MsgView
    {
        public int Ptr { get; }
        public int Delimiter { get; }
        public int WriteDelimiter { get; } 
        public ElasticBuffer Buffer { get; }
        
        public AsciiView (
            FixDefinitions definitions,
            SegmentDescription segment,
            ElasticBuffer buffer,
            Structure? structure,
            int ptr,
            int delimiter,
            int writeDelimiter) : base(definitions, segment, structure)
        {
            Ptr = ptr;
            Delimiter = delimiter;
            WriteDelimiter = writeDelimiter;
            Buffer = buffer;
        }

        protected override MsgView Create(SegmentDescription singleton)
        {
            return new AsciiView(Definitions,
                singleton,
                Buffer,
                Structure,
                Ptr,
                Delimiter,
                WriteDelimiter);
        }
    }
}
