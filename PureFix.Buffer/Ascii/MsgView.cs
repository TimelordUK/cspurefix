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
using PureFix.Tag;


namespace PureFix.Buffer.Ascii
{
    public abstract class MsgView
    {
        public FixDefinitions Definitions { get; }
        public SegmentDescription? Segment { get; }
        public Structure? Structure { get; }
        public Tags? Tags => Structure?.Tags;
        // do not allocate unless tags are fetched as a view may be used to fetch a component 
        // without fetching a specific tag.
        protected TagPos[]? SortedTagPosForwards;
        protected Dictionary<int, Range>? TagSpans;

        protected MsgView(FixDefinitions definitions, SegmentDescription segment, Structure? structure)
        {
            Definitions = definitions;
            Segment = segment;
            Structure = structure;
        }

        private void EnumeratSpan()
        {
            if (TagSpans != null) return;
            if (Tags == null) return;
            if (Structure == null) return;
            if (Segment == null) return;

            var end = Segment.EndPosition + 1;
            var start = Segment.StartPosition;
            SortedTagPosForwards = Structure.Value.Tags.Slice(start, end);
            Array.Sort(SortedTagPosForwards, TagPos.Compare);
            var span = SortedTagPosForwards[..SortedTagPosForwards.Length];
            TagSpans = [];

            for (var i = 0; i < SortedTagPosForwards.Length; ++i)
            {
                var t = span[i];
                if (TagSpans.TryGetValue(t.Tag, out var c))
                {
                    TagSpans[t.Tag] = new Range(c.Start, i);
                }
                else
                {
                    TagSpans[t.Tag] = new Range(i, i);
                }
            }
        }

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

        public string?[]? GetStrings()
        {
            return AllStrings();
        }

        public string?[]? GetStrings(int tag)
        {
            var range = GetPositions(tag);
            if (range == null) return null;
            if (SortedTagPosForwards == null) return null;
            var i = range.Value.Start.Value;
            var j = range.Value.End.Value;
            var numbers = Enumerable.Range(i, j - i + 1);
            return numbers.Select(k => SortedTagPosForwards[k].Position).Select(StringAtPosition).ToArray();
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
            if (TagSpans == null) EnumeratSpan();
            if (TagSpans == null) return -1;
            if (SortedTagPosForwards == null) return -1;
            if (TagSpans.TryGetValue(tag, out var r))
            {
                return SortedTagPosForwards[r.Start.Value].Position;
            }
            return -1;
        }

        private string?[]? AllStrings()
        {
            if (Segment == null) return null;
            var range = new int[Segment.EndPosition - Segment.StartPosition +1];
            var j = 0;
            for (var i = Segment.StartPosition; i <= Segment.EndPosition; ++i)
            {
                range[j++] = i;
            }
            return range.Select(StringAtPosition).ToArray();
        }

        protected Range? GetPositions(int tag)
        {
            EnumeratSpan();
            if (TagSpans == null) return null;
            if (!TagSpans.TryGetValue(tag, out var r))
            {
                return null;
            }

            return r;
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
            if (Structure == null) return "";
            var buffer = new StringBuilder();
            var tags = Structure.Value.Tags;
            if (Segment == null) return "";
            var count = Segment.EndPosition - Segment.StartPosition;
            var simple = Definitions.TagToSimple;

            for (var i = Segment.StartPosition; i <= Segment.EndPosition; ++i)
            {
                var tagPos = tags[i];
                simple.TryGetValue(tagPos.Tag, out var field);
                var val = StringAtPosition(i) ?? "";
                // [0] 8 (BeginString) = FIX4.4
                var token = field != null
                    ? getToken(field, val, i - Segment.StartPosition, count, tagPos)
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
