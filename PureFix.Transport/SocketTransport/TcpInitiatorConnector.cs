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
    public class TcpInitiatorConnector(
        ISessionFactory sessionFactory,
        IFixConfig config,
        IFixClock clock,
        ILogFactory logFactory)
        : BaseTcpEntity(sessionFactory, config, clock, logFactory)
    {
        // Try to connect to an endpoint, once the transport is established create a session from the factory
        // and start, which will initiate a logon to the remote.
        // On disconnect, wait ReconnectSeconds and retry.

        public override async Task Start(CancellationToken cancellationToken)
        {
            var reconnectSeconds = m_config.Description?.Application?.ReconnectSeconds ?? 30;

            m_logger.Info("TcpInitiatorConnector starts. ReconnectSeconds={ReconnectSeconds}", reconnectSeconds);

            // Create session once - it will be reused across reconnections
            // This preserves app state and session store (sequence numbers)
            FixSession? session = null;
            var isFirstConnection = true;

            // Outer loop for reconnection after session ends
            while (!cancellationToken.IsCancellationRequested)
            {
                var client = new ClientSocketTransport(m_config, m_clock, m_logFactory);
                var connected = false;
                var attempt = 1;
                var timer = new PeriodicTimer(TimeSpan.FromSeconds(reconnectSeconds));

                // Inner loop for initial connection attempts
                while (!cancellationToken.IsCancellationRequested && !connected)
                {
                    try
                    {
                        m_logger.Info("attempting to connect attempt {Attempt}", attempt);
                        await client.Start(cancellationToken);
                        connected = true;
                    }
                    catch (SocketException ex)
                    {
                        m_logger.Error(ex);
                        m_logger.Info("waiting {ReconnectSeconds}s for reconnection attempt", reconnectSeconds);
                        await timer.WaitForNextTickAsync(cancellationToken);
                        // Need fresh socket for retry
                        client = new ClientSocketTransport(m_config, m_clock, m_logFactory);
                        ++attempt;
                    }
                }

                if (!connected || cancellationToken.IsCancellationRequested)
                    break;

                if (isFirstConnection)
                {
                    m_logger.Info("connected to endpoint, creating session.");
                    session = m_sessionFactory.MakeSession();
                    isFirstConnection = false;
                }
                else
                {
                    m_logger.Info("reconnected to endpoint, reusing session.");
                    session!.PrepareForReconnect();
                }

                m_logger.Info("running the session.");

                try
                {
                    await session!.Run(client, cancellationToken);
                }
                catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
                {
                    m_logger.Warn("session ended with exception: {Message}", ex.Message);
                }

                m_logger.Info("session has ended.");

                if (cancellationToken.IsCancellationRequested)
                    break;

                m_logger.Info("waiting {ReconnectSeconds}s before reconnection attempt", reconnectSeconds);
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(reconnectSeconds), cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            m_logger.Info("TcpInitiatorConnector stopped.");
        }
    }
}
