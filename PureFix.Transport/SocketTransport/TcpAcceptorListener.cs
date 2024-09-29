using Microsoft.VisualBasic;
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
    public class TcpAcceptorListener : BaseTcpEntity
    {
        public TcpAcceptorListener(ISessionFactory sessionFactory, IFixConfig config, IFixClock clock, ILogFactory logFactory)
            : base(sessionFactory, config, clock, logFactory)
        {
        }

        public override async Task Start(CancellationToken cancellationToken)
        {
            var host = m_config?.Description?.Application?.Tcp?.Host;
            var port = m_config?.Description?.Application?.Tcp?.Port;

            ArgumentNullException.ThrowIfNull(host);
            ArgumentNullException.ThrowIfNull(port);
            ArgumentNullException.ThrowIfNull(m_config);

            var endPoint = BaseTcpTransport.MakeEndPoint(host, port.Value);
            ArgumentNullException.ThrowIfNull(endPoint);
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
                    m_logger.Info("received a new connection - create a transport");
                    var transport = new ServerSocketTransport(handle, m_config, m_clock, m_logFactory);
                    await transport.AsStream();
                    var session = m_sessionFactory.MakeSession();
                    await session.Run(transport, cancellationToken);
                });
            }
        }
    }
}
