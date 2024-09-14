using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        public class IndexVisitor : ISetDispatchReceiver
        {
            private readonly Queue<IContainedSet> _work = [];
            private readonly HashSet<IContainedSet> _visited = [];
            private readonly List<(string name, double elapsed)> _elapsed = [];
            private readonly Stopwatch _sw = new();
            public IReadOnlyList<(string name, double elapsed)> Elapsed => _elapsed;
            public int Count { get; private set; } = 0;


            public void Compute(FixDefinitions definitions)
            {
                _sw.Start();
                foreach (var msg in definitions.Message.Values)
                {
                    _work.Enqueue(msg);
                }
                Work();
            }

            private void Work()
            {
                while (_work.Count > 0)
                {
                    var set = _work.Dequeue();
                    if (_visited.Contains(set)) continue;
                    ++Count;
                    set.Index();
                    _elapsed.Add((set.Name, _sw.Elapsed.TotalMilliseconds));
                    set.Iterate(this);
                    _elapsed.Add((set.Name, _sw.Elapsed.TotalMilliseconds));
                    ++Count;
                    _visited.Add(set);
                }
            }

            public void OnComponent(ContainedComponentField cf, int index)
            {
                if (cf.Definition != null)
                {
                    _work.Enqueue(cf.Definition);
                }
            }

            public void OnGroup(ContainedGroupField cf, int index)
            {
                if (cf.Definition != null)
                {
                    _work.Enqueue(cf.Definition);
                }
            }

            public void OnSimple(ContainedSimpleField sf, int index, object? peek)
            {
            }
        }
    }
}
