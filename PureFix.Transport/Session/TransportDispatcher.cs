using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    public class TransportDispatcher
    {
        private readonly IMessageTransport m_transport;
        private readonly ILogger? m_logger;

        public TransportDispatcher(ILogFactory? logger, IMessageTransport transport)
        {
            m_transport = transport;
            m_logger = logger?.MakeLogger(nameof(TransportDispatcher));
        }

        public Task Dispatch(ISessionEventReciever reciever, CancellationToken token)
        {
            // use a pool here
            var buffer = new byte[50 * 1024];
            bool terminated = false;
            // Use .Unwrap() to properly await the inner async task
            return Task.Factory.StartNew(async () =>
            {
                m_logger?.Info("starting to relay transport to session.");
                while (!terminated && m_transport.Connected && !token.IsCancellationRequested)
                {
                    var received = await m_transport.ReceiveAsync(buffer, token);
                    if (received == 0)
                    {
                        m_logger?.Info("read 0 from transport, exit");
                        terminated = true;
                        continue;
                    }
                    var newBuffer = ArrayPool<byte>.Shared.Rent(received);
                    System.Buffer.BlockCopy(buffer, 0, newBuffer, 0, received);
                    reciever.OnRx(newBuffer, received);
                }
                m_logger?.Info($"transport has ended. Connected = {m_transport.Connected}, token =  {token.IsCancellationRequested}, terminated = {terminated}");
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();
        }
    }
}
