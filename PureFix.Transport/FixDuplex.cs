using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PureFix.Transport
{
    public abstract class FixDuplex<T>
    {
        public abstract ValueTask WriteAsync(T item, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default);
        public abstract bool TryPeek(out T? item);
        public abstract ValueTask<T> ReadAsync(CancellationToken cancellationToken = default);
        public abstract void Complete(Exception? error = default);
    }

    public class TestDuplex<T> : FixDuplex<T>
    {
        private readonly BlockingCollection<T> _q = new();

        public override ValueTask WriteAsync(T item, CancellationToken cancellationToken = default)
        {
            _q.Add(item, cancellationToken);
            return ValueTask.CompletedTask;
        }

        public async override IAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default)
        {
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
