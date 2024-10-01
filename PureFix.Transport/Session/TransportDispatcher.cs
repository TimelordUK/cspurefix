using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public class TransportDispatcher
    {
        private readonly IMessageTransport m_transport;

        public TransportDispatcher(IMessageTransport transport)
        {
            m_transport = transport;
        }

        public Task Dispatch(ISessionEventReciever reciever, CancellationToken token)
        {
            // use a pool here
            var buffer = new byte[50 * 1024];
            Task task = Task.Factory.StartNew(async () =>
            {
                while (m_transport.Connected && !token.IsCancellationRequested)
                {
                    var received = await m_transport.ReceiveAsync(buffer, token);
                    var trimmed = buffer[..received];
                    reciever.OnRx(trimmed);
                }
            },
                TaskCreationOptions.LongRunning);
            return task;
        }
    }
}
