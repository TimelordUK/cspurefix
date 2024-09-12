using System;
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
}
