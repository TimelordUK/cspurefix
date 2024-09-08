using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types.tag;


namespace PureFix.Buffer.Ascii
{
    public abstract class MsgView(FixDefinitions definitions, SegmentDescription segment, Structure? structure)
    {
        public FixDefinitions Definitions => definitions;
        public SegmentDescription? Segment => segment;
        public Structure? Structure => structure;
        public Tags? Tags => Structure?.Tags;
        protected List<TagPos>? SortedTagPosForwards;
        protected List<TagPos>? SortedTagPosBackwards;
        
        // list of tags that must be present
        public int[] Missing()
        {
            if (Segment == null) return [];
            if (Segment.Set == null) return [];
            return MissingRequired(Segment.Set, []).ToArray();
        }

        protected static string AsToken(SimpleFieldDefinition? field, string val, int i, int count, TagPos tagpos)
        {
            var perLine = 2;
            var newLine = Environment.NewLine;
            // [280] 814 (ApplQueueResolution) = 2[OverlayLast][281] 10 (CheckSum) = 80
            string desc;
            string name;
            if (field != null)
            {
                name = field.Name;
                desc = field.IsEnum ? $"{val} [{field.ResolveEnum(val)}]" : $"{val}";
            } else
            {
                desc = $"{val}";
                name = "unknown";
            }
            string delimiter;
            if (i == 1 || (i < count && i % perLine - 1 == 0))
            {
                delimiter = newLine;
            } else
            {
                delimiter = i < count ? ", " : "";
            }

            return $"[{i}] {tagpos.Tag} ({name}) = {desc}{delimiter}";
        }
        
        private List<int> MissingRequired(IContainedSet? segmentSet, List<int> start)
        {
            if (segmentSet == null)
            {
                return start;
            }
            return segmentSet.Fields.Aggregate(start, (tags, field) =>
            {
                switch (field.Type)
                {
                    case ContainedFieldType.Simple:
                        MissingSimple((ContainedSimpleField)field, tags);
                        break;

                    case ContainedFieldType.Group:
                        MissingGroup(segmentSet, (ContainedGroupField)field, tags);
                        break;

                    case ContainedFieldType.Component:
                        MissingComponent((ContainedComponentField)field, tags);
                        break;
                }
                return tags;
            });
        }

        private void MissingGroup(IContainedSet def , ContainedGroupField gf, List<int> tags)
        {
            var name = gf.Definition?.NoOfField != null ? gf.Definition.NoOfField.Name : def.Name;
            var groupView = GetView(name) ?? GetView(gf.Definition?.Name ?? "");
            if (groupView == null)
            {
                return;
            }

            var count = groupView.GroupCount();
            for (var j = 0; j < count; ++j)
            {
                var instance = groupView.GetGroupInstance(j);
                instance?.MissingRequired(gf.Definition, tags);
            }
        }

        public int GroupCount()
        {
            var count = Segment?.DelimiterPositions.Count ?? 0;
            return count;
        }

        public string? GetString(int tag)
        {
            var position = GetPosition(tag);
            return position < 0 ? null : StringAtPosition(position);
        }

      

        /**
         * if this view represents a repeated group then return a sub view representing
         * this instance of repeated group.
         * @param i the index to return i.e. 0 is first within repeated group
         */
        public MsgView? GetGroupInstance(int i)
        {
            var instance = Segment?.GetInstance(i);
            return instance == null ? null : Create(instance);
        }

        private void MissingComponent(ContainedComponentField cf, List<int> ints)
        {
            var view = GetView(cf.Name);
            view?.MissingRequired(cf.Definition, ints);
        }

        private void MissingSimple(ContainedSimpleField sf, List<int> a) {
            if (sf.Required && GetPosition(sf.Definition.Tag) < 0)
            {
                a.Add(sf.Definition.Tag);
            }
        }

        public MsgView? GetView(string name)
        {
            var parts = name.Split('.');
            return parts.Aggregate(this, (a, current) =>
            {
                var subStructure = a.Structure;
                if (a.Segment == null)
                {
                    return a;
                }

                var singleton = subStructure?.FirstContainedWithin(current, a.Segment);
                if (singleton != null)
                {
                    return a.Create(singleton);
                }

                if (a?.Segment?.Set?.LocalNameToField.TryGetValue(current, out var component) ?? false)
                {
                    var abbreviation = subStructure?.FirstContainedWithin(component.Name, a.Segment);
                    if (abbreviation != null)
                    {
                        return a.Create(abbreviation);
                    }
                }
                return null;
            });
        }

        protected int GetPosition(int tag)
        {
            var pos = BinarySearch(tag);
            if (pos >= 0)
            {
                return SortedTagPosForwards?[pos].Position ?? -1;
            }
            return -1;
        }

        private string?[] AllStrings()
        {
            if (Segment == null) return [];
            var range = new int[Segment.EndPosition - Segment.StartPosition +1];
            var j = 0;
            for (var i = segment.StartPosition; i <= segment.EndPosition; ++i)
            {
                range[j++] = i;
            }
            return range.Select(StringAtPosition).ToArray();
        }

        protected int[] GetPositons(int tag)
        {
            var forwards = SortedTagPosForwards;
            var backwards = SortedTagPosBackwards;
            var position = BinarySearch(tag);
            if (forwards == null || backwards == null || position < 0)
            {
                return [];
            }

            var count = forwards.Count;
            var last = count - 1;
            var end = position;
            while (end <= last)
            {
                if (tag != forwards[end].Tag)
                {
                    break;
                }
                ++end;
            }

            // avoid backtracking over an array by scan forwards on a reversed copy
            var start = last - position;
            while (start <= last)
            {
                if (tag != backwards[start].Tag)
                {
                    break;
                }
                ++start;
            }

            var begin = last - (start - 1);
            var len = end - begin;

            var positions = forwards[begin..end].Select(s => s.Position).ToArray();
            return positions;
        }

        private int BinarySearch(int tag)
        {
            if (Structure == null) return -1;
            if (SortedTagPosForwards != null) return TagPos.BinarySearch(SortedTagPosForwards, tag);
            SortedTagPosForwards = Structure.Tags.Slice(segment.StartPosition, segment.EndPosition + 1);
            SortedTagPosForwards.Sort(TagPos.Compare);
            SortedTagPosBackwards = SortedTagPosForwards[..SortedTagPosForwards.Count];
            SortedTagPosBackwards.Reverse();
            return TagPos.BinarySearch(SortedTagPosForwards, tag);
        }

        /**
         * easy human-readable format showing each field, its position, value and resolved
         * enum.
         */
        public override string ToString()
        {
            return Stringify(AsToken);
        }

        private string Stringify(Func<SimpleFieldDefinition, string, int, int, TagPos, string> getToken)
        {
            if (structure == null) return "";
            var buffer = new StringBuilder();
            var tags = structure.Tags;
            var count = segment.EndPosition - segment.StartPosition;
            var simple = Definitions.TagToSimple;

            for (var i = segment.StartPosition; i <= segment.EndPosition; ++i)
            {
                var tagPos = tags[i];
                simple.TryGetValue(tagPos.Tag, out var field);
                var val = StringAtPosition(i) ?? "";
                // [0] 8 (BeginString) = FIX4.4
                var token = field != null
                    ? getToken(field, val, i - segment.StartPosition, count, tagPos)
                    : $"[{i}] {tagPos.Tag} (unknown) = {val}, ";
                buffer.Append(token);
            }

            return buffer.ToString();
        }

        //public abstract T? GetTyped<T>(int tag);
        protected abstract MsgView Create(SegmentDescription singleton);
        protected abstract string? StringAtPosition(int position);
    }
}
