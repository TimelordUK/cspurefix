using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public class ContainedFieldCollector : ISetDispatchReceiver
    {
        private static Dictionary<IContainedSet, IReadOnlyList<(IContainedSet parent, ContainedField child)>> _memoised = [];
        private List<(IContainedSet parent, ContainedField child)> _fields = [];
        private IReadOnlyList<(IContainedSet parent, ContainedField child)> Fields => _fields;
        public Queue<IContainedSet> _queue = [];
        IContainedSet? _current;

        public IReadOnlyList<(IContainedSet parent, ContainedField child)> Compute(IContainedSet set)
        {
            if (_memoised.TryGetValue(set, out var cached))
            {
                return cached;
            }
            _fields.Clear();
            _queue.Enqueue(set);
            Work();
            _memoised[set] = _fields[0..];
            return Fields;
        }

        private void Work()
        {
            while (_queue.Count > 0)
            {
                _current = _queue.Dequeue();
                _current.Iterate(this);
            }
        }

        public void OnComponent(ContainedComponentField cf, int index)
        {
            if (cf.Definition == null) return;
            if (_current == null) return;
            _fields.Add((_current, cf));
            var c = new ContainedFieldCollector();
            var res = c.Compute(cf.Definition);
            _fields.AddRange(res);
        }

        public void OnGroup(ContainedGroupField cf, int index)
        {
            if (cf.Definition == null) return;
            if (_current == null) return;
            _fields.Add((_current, cf));
            var c = new ContainedFieldCollector();
            var res = c.Compute(cf.Definition);
            _fields.AddRange(res);
        }

        public void OnSimple(ContainedSimpleField sf, int index, object? peek)
        {
            if (_current == null) return;
            _fields.Add((_current, sf));
        }
    }
}
