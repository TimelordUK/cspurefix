using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class LogTrace : IEnumerable<string>
    {
        private object _lock = new object();
        private List<string> _lines = [];

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _lines.Count;
                }
            }
        }

        public void Add(string message)
        {
            lock (_lock)
            {
                _lines.Add(message);
            }
        }

        public IReadOnlyList<string> GetEntries()
        {
            lock (_lock)
            {
                return new List<string>(_lines);
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
           return GetEntries().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
