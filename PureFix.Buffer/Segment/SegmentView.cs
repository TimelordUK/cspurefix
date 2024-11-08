using PureFix.Dictionary.Contained;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Segment
{
    public class SegmentView
    {
        public string Name { get; } 
        public int EndTag { get; private set; }
        public int EndPosition { get; private set; } = int.MinValue;
        public int StartPosition { get; private set; } = int.MaxValue;
        public int StartTag { get; private set; }
        public IContainedSet Set { get; }
        private readonly List<TagPos> _tags;
        public IReadOnlyList<TagPos> Tags => _tags;

        public SegmentView(string name, IContainedSet set)
        {
            Name = name;
            Set = set;
            _tags = new List<TagPos>(set.FlattenedTag.Count);
        }

        public void Add(TagPos tag)
        {
            if (tag.Position < StartPosition)
            {
                StartPosition = tag.Position;
                StartTag = tag.Tag;
            }

            if (tag.Position > EndPosition)
            {
                EndPosition = tag.Position;
                EndTag = tag.Tag;
            }

            _tags.Add(tag);
        }
    }
}
