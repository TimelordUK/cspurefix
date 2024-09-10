using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PureFix.Transport
{
    public class ChannelDuplex<T> : FixDuplex<T>
    {
        private readonly Channel<T> _channel = Channel.CreateUnbounded<T>();
        public ChannelReader<T> Reader => _channel.Reader;
        public ChannelWriter<T> Writer => _channel.Writer;

        public override ValueTask WriteAsync(T item, CancellationToken cancellationToken = default)
        {
            return Writer.WriteAsync(item, cancellationToken);
        }

        public override IAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default)
        {
            return Reader.ReadAllAsync(cancellationToken);
        }

        public override bool TryPeek(out T? item)
        {
            return Reader.TryPeek(out item);
        }

        public override ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            return Reader.ReadAsync(cancellationToken);
        }

        public override void Complete(Exception? error = default)
        {
            Writer.Complete(error);
        }
    }
}
