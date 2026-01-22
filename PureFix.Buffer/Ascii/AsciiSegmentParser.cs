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
using PureFix.Types;
using PureFix.Types.Core;


namespace PureFix.Buffer.Ascii
{
    /// <summary>
    /// Stack-based segment parser using recursive descent.
    /// Fast and efficient for messages with tags in definition order.
    /// </summary>
    public partial class AsciiSegmentParser(IFixDefinitions definitions) : ISegmentParser
    {
        public IFixDefinitions Definitions { get; } = definitions;

        public Structure? Parse(string msgType, Tags tags, int last)
        {
            if (!Definitions.Message.TryGetValue(msgType, out var msgDefinition))
            {
                return null;
            }

            // in process of being discovered and may have any amount of depth
            // i.e. a component containing a repeated group of components
            // with sub-groups of components
            using var ti = TagIndex.Rent(msgDefinition, tags, last);
            using var context = Context.Rent(msgDefinition, tags, last);

            var msgStructure = new SegmentDescription(msgDefinition.Name, tags[0].Tag, msgDefinition,
                  context.CurrentTagPosition, context.StructureStack.Count, SegmentType.Msg);
            context.StructureStack.Push(msgStructure);
            Discover(context);
            // for any level root level component, cant guarantee tags will be in a contiguous block so
            // switch to using the index
            Clean(context);
            Fragments(context, ti);
            // now know where all components and groups are positioned within message
            // Structure copies segments, so context can be returned to pool
            return new Structure(tags, context.Segments);
        }

        private static void Fragments(Context context, TagIndex ti)
        {
            // Optimization: only build SegmentViews for components detected as fragmented
            // during Discover. Non-fragmented components use their position ranges directly.
            if (context.FragmentedComponents.Count == 0) return;

            var seen = new HashSet<string>();
            for (var i = 1; i < context.Segments.Count -1; ++i)
            {
                var seg = context.Segments[i];
                if (seg.Depth != 1) continue;
                if (seg.Name == null) continue;
                if (seg.SegmentView != null) continue;
                if (seen.Contains(seg.Name)) continue;
                // Only process components that were detected as fragmented
                if (!context.FragmentedComponents.Contains(seg.Name)) continue;
                // case where there is a component with one field which wraps the group - these will all be self contained.
                if (ti.ComponentGroupWrappers.Contains(seg.Name)) continue;
                // for example instrument where tags may be scattered and not all contiguous, compute a view rather than a slice.
                var v = ti.GetInstance(seg.Name);
                if (v == null) continue;
                seen.Add(seg.Name);
                seg.Add(v);
            }
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

                // Track when we exit depth-1 components for fragmentation detection
                if (done.Depth == 1 && done.Name != null)
                {
                    context.ExitedDepth1Components.Add(done.Name);
                }

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
                        // Detect fragmentation: re-entering a depth-1 component we already exited
                        if (context.StructureStack.Count == 1 && context.ExitedDepth1Components.Contains(cf.Name))
                        {
                            context.FragmentedComponents.Add(cf.Name);
                        }
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
