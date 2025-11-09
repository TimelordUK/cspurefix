using System.Collections;

namespace PureFix.Test.ModularTypes.Helpers
{
    internal class LogTrace : IEnumerable<string>
    {
        private readonly object _lock = new object();
        private readonly List<string> _lines = [];

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
