using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Transport.SocketTransport
{
    public class TcpAcceptorListener(
        ISessionFactory sessionFactory,
        IFixConfig config,
        IFixClock clock,
        ILogFactory logFactory)
        : BaseTcpEntity(sessionFactory, config, clock, logFactory)
    {
        public override async Task Start(CancellationToken cancellationToken)
        {
            var host = m_config?.Description?.Application?.Tcp?.Host;
            var port = m_config?.Description?.Application?.Tcp?.Port;

            if (host == null) throw new InvalidOperationException("No host configured for TCP endpoint.");
            if (port == null) throw new InvalidOperationException("No port configured for TCP endpoint.");
            if (m_config == null) throw new InvalidOperationException("Configuration not initialized.");

            m_logger.Info("TcpAcceptorListener starts.");

            var endPoint = BaseTcpTransport.MakeEndPoint(host, port.Value);
            if (endPoint == null) throw new InvalidOperationException("Failed to create endpoint from host and port.");
            using Socket listener = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            m_logger.Info($"binding to endpoint {endPoint}");
            listener.Bind(endPoint);
            m_logger.Info("listening for new connections.");
            listener.Listen();

            while (!cancellationToken.IsCancellationRequested)
            {
                var handle = await listener.AcceptAsync(cancellationToken);
                await Task.Factory.StartNew(async () =>
                {
                    m_logger.Info($"received a new connection - create a transport. remote = {handle.RemoteEndPoint}");
                    var transport = new ServerSocketTransport(handle, m_config, m_clock, m_logFactory);
                    await transport.AsStream();
                    var session = m_sessionFactory.MakeSession();
                    await session.Run(transport, cancellationToken);
                }, cancellationToken);
            }
        }
    }
}
