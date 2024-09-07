using System;
using System.Collections.Generic;
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

        protected abstract MsgView Create(SegmentDescription singleton);
    }
}
