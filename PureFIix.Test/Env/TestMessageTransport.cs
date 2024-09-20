using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Transport.Session;

namespace PureFIix.Test.Env
{
    internal class TestMessageTransport : IMessageTransport
    {
        private readonly BlockingCollection<byte[]> _rx_data = new ();
        private BlockingCollection<byte[]> _tx_data;

        public void ConnectTo(TestMessageTransport sendingTo)
        {
            _tx_data = sendingTo._rx_data;
        }

        public Task SendAsync(ReadOnlySpan<byte> messageBytes, CancellationToken token)
        {
            _tx_data?.Add(messageBytes.ToArray(), token);
           return Task.CompletedTask;
        }

        public Task<int> ReceiveAsync(Span<byte> buffer, CancellationToken token)
        {
            var b = _rx_data.Take(token);
            b.CopyTo(buffer);
            return Task.FromResult(b.Length);
        }
    }
}
