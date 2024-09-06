using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Types.tag;

namespace PureFix.Buffer.Ascii
{
    public class AsciiSegmentParser(FixDefinitions definitions)
    {
        public FixDefinitions Definitions { get; } = definitions;
        private Tags _tags { get; }
        private readonly List<SegmentDescription> _segments = new ();
        private readonly Stack<SegmentDescription> _structureStack = new ();

        public Structure? Parse(string msgType, Tags tags, int last)
        {
            if (!Definitions.Message.TryGetValue(msgType, out var msgDefinition))
            {
                return null;
            }

            // in process of being discovered and may have any amount of depth
            // i.e. a component containing a repeated group of components
            // with sub-groups of components
            
            _structureStack.Clear();
            _segments.Clear();
            var currentTagPosition = 0;
            SegmentDescription? peek;

            return null;
        }

        private void Unwind(int tag, int currentTagPosition, Tags tags)
        {
            while (_structureStack.Count > 1)
            {
                var done = _structureStack.Pop();
                done.End(_segments.Count, currentTagPosition - 1, tags[currentTagPosition - 1].Tag);
                _segments.Add(done);
                var peek = _structureStack.Peek();
                if (peek.Set != null && peek.Set.ContainedTag.ContainsKey(tag))
                {
                    // unwound to point this tag lives in this set.
                    break;
                }

                if (peek.Type == SegmentType.Msg)
                {
                    // this is unknown tag, and it is not part of trailer so raise unknown
                    break;
                }
            }
        }
    }
}
