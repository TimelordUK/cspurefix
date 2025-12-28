using PureFix.Dictionary.Contained;
using PureFix.Types;
using PureFix.Types.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Segment
{
    public class SegmentView(string name, IContainedSet set)
    {
        public string Name { get; } = name;
        public int EndTag { get; private set; }
        public int EndPosition { get; private set; } = int.MinValue;
        public int StartPosition { get; private set; } = int.MaxValue;
        public int StartTag { get; private set; }
        public IContainedSet Set { get; } = set;
        private readonly List<TagPos> _tags = new(set.FlattenedTag.Count);
        public IReadOnlyList<TagPos> Tags => _tags;

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
