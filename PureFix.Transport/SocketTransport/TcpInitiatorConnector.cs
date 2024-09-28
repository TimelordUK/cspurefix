using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PureFix.Transport.SocketTransport
{
    internal class TcpInitiatorConnector
    {
        private readonly ClientSocketTransport m_client;
        private readonly ILogger m_logger;
        private readonly ISessionFactory m_sessionFactory;

        public TcpInitiatorConnector(ISessionFactory sessionFactory, IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(sessionFactory);
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);
            m_client = new ClientSocketTransport(config, clock, logFactory);
            m_logger = logFactory.MakeLogger("TcpInitiatorConnector");
            m_sessionFactory = sessionFactory;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            bool connected = false;
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));
            while (!cancellationToken.IsCancellationRequested && false == connected)
            {
                try
                {
                    m_logger.Info($"attempting to connect");
                    await m_client.Connect(cancellationToken);
                    connected = true;
                }
                catch (SocketException ex)
                {
                    m_logger.Error(ex);
                    m_logger.Info($"waiting for re-connecton attempt");
                    await timer.WaitForNextTickAsync(cancellationToken);
                }
            }
            m_logger.Info("connected to endpoint starting a new session.");
            var session = m_sessionFactory.MakeSession();
            await session.Run(m_client, cancellationToken);
            m_logger.Info("session has ended.");
        }
    }
}
