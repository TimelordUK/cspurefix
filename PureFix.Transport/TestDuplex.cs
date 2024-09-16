using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace PureFix.Transport
{
    public class TestDuplex<T> : FixDuplex<T>
    {
        private readonly BlockingCollection<T> _q = new();

        public override ValueTask WriteAsync(T item, CancellationToken cancellationToken = default)
        {
            _q.Add(item, cancellationToken);
            return ValueTask.CompletedTask;
        }

        public async override IAsyncEnumerable<T> ReadAllAsync([EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            await Task.Yield();
            foreach (var i in _q)
            {
                yield return i;
            }
        }

        public override bool TryPeek(out T? item)
        {
            return _q.TryTake(out item);
        }

        public override ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            var r = _q.Take();
            return ValueTask.FromResult(r);
        }

        public override void Complete(Exception? error = default)
        {
            _q.CompleteAdding();
        }
    }
}
