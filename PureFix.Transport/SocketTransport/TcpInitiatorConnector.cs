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
    public class TcpInitiatorConnector : BaseTcpEntity
    {
        private readonly ClientSocketTransport m_client;

        public TcpInitiatorConnector(ISessionFactory sessionFactory, IFixConfig config, IFixClock clock, ILogFactory logFactory) 
            : base(sessionFactory, config, clock, logFactory)
        {
            m_client = new ClientSocketTransport(config, clock, logFactory);
        }

        // try to connect to an endpoint, once the transport is established create a session from the factory
        // and start, which will initiate a logon to the remote.

        public override async Task Start(CancellationToken cancellationToken)
        {
            bool connected = false;
            int attempt = 1;
            var retrySecs = 30;
            
            m_logger.Info("TcpInitiatorConnector starts.");

            var timer = new PeriodicTimer(TimeSpan.FromSeconds(retrySecs));
            while (!cancellationToken.IsCancellationRequested && false == connected)
            {
                try
                {
                    m_logger.Info($"attempting to connect attempt {attempt}");
                    await m_client.Start(cancellationToken);
                    connected = true;
                }
                catch (SocketException ex)
                {
                    m_logger.Error(ex);
                    m_logger.Info("waiting for re-connecton attempt");
                    await timer.WaitForNextTickAsync(cancellationToken);
                    ++attempt;
                }
            }
            m_logger.Info($"connected to endpoint make a new session.");
            var session = m_sessionFactory.MakeSession();
            m_logger.Info("running the new session.");
            await session.Run(m_client, cancellationToken);
            m_logger.Info("session has ended.");
        }
    }
}
