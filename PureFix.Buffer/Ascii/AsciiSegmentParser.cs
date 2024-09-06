using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types.tag;

namespace PureFix.Buffer.Ascii
{
    public class AsciiSegmentParser(FixDefinitions definitions)
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

        public class Context(string msgType, Tags tags, int last)
        {
            public string MsgType { get; } = msgType;
            public List<SegmentDescription> Segments { get; } = new();
            public Stack<SegmentDescription> StructureStack { get; } = new();
            public int CurrentTagPosition { get; set; }
            public Tags Tags { get; } = tags;
            public int Last { get; } = last;
            public MessageDefinition? MsgDefinition { get; set; }
            public SegmentDescription? Peek { get; set; }
        }

        public FixDefinitions Definitions { get; } = definitions;
        
        public Structure? Parse(string msgType, Tags tags, int last)
        {
            if (!Definitions.Message.TryGetValue(msgType, out var msgDefinition))
            {
                return null;
            }

            // in process of being discovered and may have any amount of depth
            // i.e. a component containing a repeated group of components
            // with sub-groups of components
            var context = new Context(msgType, tags, last) { MsgDefinition = msgDefinition };
            
            return null;
        }

        public Summary Summarise(Context context)
        {
            return new Summary(context.MsgType,
                context.Tags.ToArray(),
                context.Last,
                context.MsgDefinition?.ToString() ?? "na",
                context.CurrentTagPosition,
                context.StructureStack.Peek().ToString(),
                context.Segments.Select(s => s.ToString()).ToArray(),
                context.Segments.Select(s => s.ToString()).ToArray());
        }

        private void Unwind(int tag, Context context)
        {
            while (context.StructureStack.Count > 1)
            {
                var done = context.StructureStack.Pop();
                done.End(context.Segments.Count, context.CurrentTagPosition - 1, context.Tags[context.CurrentTagPosition - 1].Tag);
                context.Segments.Add(done);
                context.Peek = context.StructureStack.Peek();
                if (context.Peek.Set != null && context.Peek.Set.ContainedTag.ContainsKey(tag))
                {
                    // unwound to point this tag lives in this set.
                    break;
                }

                if (context.Peek.Type == SegmentType.Msg)
                {
                    // this is unknown tag, and it is not part of trailer so raise unknown
                    break;
                }
            }
        }

        private SegmentDescription? Examine(int tag, Context context)
        {
            SegmentDescription? structure = null;
            var currentField = context.Peek?.CurrentField;
            if (currentField == null) return null;

            switch (currentField.Type)
            {
                case ContainedFieldType.Simple:
                {
                    var sf = (ContainedSimpleField)currentField;
                    if (sf.Definition.Tag == tag)
                    {
                        context.CurrentTagPosition += 1;
                    }
                    break;
                }

                // moving deeper into structure, start a new context
                case ContainedFieldType.Component:
                {
                    var cf = (ContainedComponentField)currentField;
                    structure = new SegmentDescription(cf.Name, tag, cf.Definition, context.CurrentTagPosition,
                        context.StructureStack.Count, SegmentType.Component);
                        break;
                }

                case ContainedFieldType.Group:
                {
                    var gf = (ContainedGroupField)currentField;
                    structure = new SegmentDescription(gf.Name, tag, gf.Definition, context.CurrentTagPosition,
                        context.StructureStack.Count, SegmentType.Group);
                    context.CurrentTagPosition += 1;
                        structure.StartGroup(context.Tags[context.CurrentTagPosition].Tag);
                    break;
                }

                default:
                {
                    var c = Summarise(context);
                    throw new InvalidDataException($"examine unknown type for tag = {tag} {c}");
                }
            }

            return structure;
        }
}
}
