using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public partial class AsciiSegmentParser
    {
        public class Context(MessageDefinition message, Tags tags, int last)
        {
            public string MsgType { get; } = message.MsgType;
            public List<SegmentDescription> Segments { get; } = [];
            public Stack<SegmentDescription> StructureStack { get; } = [];
            public int CurrentTagPosition { get; set; }
            public Tags Tags { get; } = tags;
            public int Last { get; } = last;
            public MessageDefinition? MsgDefinition { get; } = message;
            public SegmentDescription? Peek => StructureStack.Peek();
        }
    }
}
