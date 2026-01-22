using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;


namespace PureFix.Buffer.Segment
{
    public class SegmentDescription(
        string? name,
        int startTag,
        IContainedSet? set,
        int startPosition,
        int depth,
        SegmentType type)
    {
        public string? Name { get; } = name;
        public int Index { get; private set; }
        public int EndTag { get; private set; }
        public int? DelimiterTag { get; private set; }
        public int EndPosition { get; set; }
        public int StartPosition { get; private set; } = startPosition;
        public int Depth { get; } = depth;
        public int StartTag { get; private set; } = startTag;
        public SegmentType Type { get; } = type;
        public IContainedSet? Set { get; } = set; 
        public SegmentView? SegmentView { get; private set; }

        public ContainedField? CurrentField { get; private set; }
        private List<int>? _delimterPositions;
        private List<int>? _containedDelimiterPositions;
        private List<SegmentView>? _instanceViews;

        public IReadOnlyList<int> DelimiterPositions => _containedDelimiterPositions ?? (IReadOnlyList<int>)Array.Empty<int>();

        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.Append($"name = {Name}, ");
            buffer.Append($"startTag = {StartTag}, ");
            buffer.Append($"startPosition = {StartPosition}, ");
            buffer.Append($"type = {Type}, ");
            buffer.Append($"depth = {Depth}, ");
            buffer.Append($"index = {Index}, ");
            buffer.Append($"endTag = {EndTag}, ");
            buffer.Append($"endPosition = {EndPosition}, ");
            buffer.Append($"delimiterTag = {DelimiterTag}, ");
            buffer.Append($"delimiterPositions = {string.Join(", ", DelimiterPositions)}, ");
            buffer.Append($"currentField = {CurrentField}, ");
            buffer.Append($"set = {Set?.Keys()}, ");

            return buffer.ToString();
        }

        public bool Contains(SegmentDescription segment)
        {
            return segment.StartPosition >= StartPosition && segment.EndPosition <= EndPosition;
        }

        public SegmentDescription? GetInstance(int instance)
        {
            if (_delimterPositions == null)
            {
                return null;
            }

            if (instance < 0 || instance >= _delimterPositions.Count)
            {
                return null;
            }

            var start = _delimterPositions[instance];
            var end = instance < _delimterPositions.Count - 1
                ? _delimterPositions[instance + 1] - 1
                : EndPosition;
            var instanceName = Type == SegmentType.Batch ? Set?.Abbreviation ?? Name : Name ?? "na";
            var d = new SegmentDescription(instanceName, StartTag, Set, start, Depth, Type)
            {
                EndPosition = end,
                EndTag = EndTag
            };

            // If we have stored instance SegmentViews (from TagByTag parsing), use them
            if (_instanceViews != null && instance < _instanceViews.Count)
            {
                d.Add(_instanceViews[instance]);
            }

            return d;
        }

        /// <summary>
        /// Adds an instance SegmentView for TagByTag parsing.
        /// Call this after AddDelimiterPosition for each instance.
        /// </summary>
        public void AddInstanceView(SegmentView view)
        {
            _instanceViews ??= [];
            _instanceViews.Add(view);
        }

        public void StartGroup(int tag)
        {
            DelimiterTag = tag;
            _delimterPositions = [];
            _containedDelimiterPositions = [];
        }

        public bool AddDelimiterPosition(int position)
        {
            if (_delimterPositions == null || _containedDelimiterPositions == null)
            {
                return false;
            }

            if (_containedDelimiterPositions.Contains(position))
            {
                return false;
            }

            _delimterPositions.Add(position);
            _containedDelimiterPositions.Add(position);
            return true;
        }

        public void SetCurrentField(int tag)
        {
            if (Set == null) return;
            if (Set.LocalTag.TryGetValue(tag, out var sf))
            {
                CurrentField = sf;
            }
            else if (Set.TagToField.TryGetValue(tag, out var cf))
            {
                CurrentField = cf.field;
            }
        }

        public bool GroupAddDelimiter(int tag, int position)
        {
            var delimiter = false;
            if (Set is not GroupFieldDefinition) return delimiter;
            if (tag == DelimiterTag)
            {
                delimiter = AddDelimiterPosition(position);
            }
            return delimiter;
        }

        public void End(int i, int pos, int endTag)
        {
            Index = i;
            CurrentField = null;
            EndPosition = pos;
            EndTag = endTag;
        }

        public void Add(SegmentView segmentView)
        {
            SegmentView = segmentView;
            EndPosition = segmentView.EndPosition;
            StartPosition = segmentView.StartPosition;
            EndTag = segmentView.EndTag;
            StartTag = segmentView.StartTag;
        }
    }
}
