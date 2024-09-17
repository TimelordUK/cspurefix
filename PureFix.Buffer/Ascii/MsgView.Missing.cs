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
using PureFix.Types;


namespace PureFix.Buffer.Ascii
{
    public abstract partial class MsgView
    {
        // list of tags that must be present
        public int[] Missing()
        {
            if (Segment == null) return [];
            if (Segment.Set == null) return [];
            return [.. MissingRequired(Segment.Set, [])];
        }
        private void MissingComponent(ContainedComponentField cf, List<int> ints)
        {
            var view = GetView(cf.Name);
            view?.MissingRequired(cf.Definition, ints);
        }

        private void MissingSimple(ContainedSimpleField sf, List<int> a)
        {
            if (sf.Required && GetPosition(sf.Definition.Tag) < 0)
            {
                a.Add(sf.Definition.Tag);
            }
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

        private void MissingGroup(IContainedSet def, ContainedGroupField gf, List<int> tags)
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
    }
}
