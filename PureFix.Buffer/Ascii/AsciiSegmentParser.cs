using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Tag;



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
            var context = new Context(msgDefinition, tags, last);

            var msgStructure = new SegmentDescription(msgDefinition.Name, tags[0].Tag, msgDefinition,
                  context.CurrentTagPosition, context.StructureStack.Count, SegmentType.Msg);
            context.StructureStack.Push(msgStructure);
            Discover(context);
            Clean(context);

            // now know where all components and groups are positioned within message
            return new Structure(tags, [.. context.Segments]);
        }


        public static Summary Summarise(Context context)
        {
            return new Summary(context.MsgType,
                context.Tags.ToArray(),
                context.Last,
                context.MsgDefinition?.ToString() ?? "na",
                context.CurrentTagPosition,
                context.StructureStack.Peek().ToString(),
                context.Segments.Select(s => s.ToString()).ToArray(),
                context.StructureStack.Select(s => s.ToString()).ToArray());
        }

        private static void Unwind(Context context, int tag)
        {
            while (context.StructureStack.Count > 1)
            {
                var done = context.StructureStack.Pop();
                done.End(context.Segments.Count, context.CurrentTagPosition - 1, context.Tags[context.CurrentTagPosition - 1].Tag);
                context.Segments.Add(done);

                var peekSet = context.Peek?.Set;
                if (peekSet!= null && peekSet.ContainedTag.ContainsKey(tag))
                {
                    // unwound to point this tag lives in this set.
                    break;
                }

                if (context.Peek?.Type == SegmentType.Msg)
                {
                    // this is unknown tag, and it is not part of trailer so raise unknown
                    break;
                }
            }
        }

        private static SegmentDescription? Examine(Context context, int tag)
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

        public static bool GroupDelimiter(Context context, int tag)
        {
            var delimiter = false;
            if (context.Peek == null) return false;
            if (tag == context.Peek.DelimiterTag)
            {
                context.Peek.AddDelimiterPosition(context.CurrentTagPosition);
            }
            else if (context.StructureStack.Count > 1)
            {
                // if a group is represented by a repeated component, then the tag representing delimiter
                // needs to be added further up stack to group itself.
                var last = context.StructureStack.FirstOrDefault(static d => d.Set?.Type == ContainedSetType.Group);
                if (last != null)
                {
                    delimiter = last.GroupAddDelimiter(tag, context.CurrentTagPosition);
                }
            }
            return delimiter;
        }

        private static void Discover(Context context)
        {
            while (context.CurrentTagPosition <= context.Last)
            {
                var tag = context.Tags[context.CurrentTagPosition].Tag;
                context.Peek?.SetCurrentField(tag);
                if (GroupDelimiter(context, tag) || (context.Peek?.Set != null && !context.Peek.Set.ContainedTag.ContainsKey(tag)))
                {
                    // unravelled all way back to root hence this is not recognised
                    var unknown = context.Peek?.Type == SegmentType.Msg;
                    if (unknown)
                    {
                        Gap(context, tag);
                    }
                    else if (context.StructureStack.Count > 1)
                    {
                        // move back up the segments and save the finished group / component
                        Unwind(context, tag);
                    }
                    continue;
                }
                if (context.Peek?.CurrentField == null || context.Peek.Set == null)
                {
                    throw new InvalidDataException($"discover no currentField or set for tag = ${tag} {Summarise(context)}");
                }
                var structure = Examine(context, tag);
                if (structure != null)
                {
                    context.StructureStack.Push(structure);
                }
            }
        }

        private static void Clean(Context context)
        {
            // any remainder components can be closed.
            var segments = context.Segments;
            while (context.StructureStack.Count > 0)
            {
                var done = context.StructureStack.Pop();
                done.End(segments.Count, context.CurrentTagPosition - 1, context.Tags[context.CurrentTagPosition - 1].Tag);
                segments.Add(done);
            }
            // logically reverse the trailer and message so trailer is last in list.
            var m1 = segments.Count - 1;
            var m2 = segments.Count - 2;
            (segments[m1], segments[m2]) = (segments[m2], segments[m1]);
        }

        private static void Gap(Context context, int tag)
        {
            var gap = new SegmentDescription(".undefined", tag, context.Peek?.Set,
              context.CurrentTagPosition, context.StructureStack.Count, SegmentType.Gap);
            gap.End(context.Segments.Count, context.CurrentTagPosition, tag);
            context.Segments.Add(gap);
            context.CurrentTagPosition++;
        }
    }
}
