using PureFix.Transport.Session;
using PureFix.Types;
using System.Net.Sockets;

namespace PureFix.Transport.SocketTransport
{
    public class TcpAcceptorListener(
        ISessionFactory sessionFactory,
        IFixConfig config,
        IFixClock clock,
        ILogFactory logFactory,
        ISessionScopeFactory? scopeFactory = null)
        : BaseTcpEntity(sessionFactory, config, clock, logFactory, scopeFactory)
    {
        private int _liveConnections;
        private int _totalConnections;

        /// <summary>Connections currently being served. Exposed for tests and health checks.</summary>
        public int LiveConnections => Volatile.Read(ref _liveConnections);

        /// <summary>Connections accepted since start.</summary>
        public int TotalConnections => Volatile.Read(ref _totalConnections);

        public override async Task Start(CancellationToken cancellationToken)
        {
            var tcp = m_config?.Description?.Application?.Tcp;
            var host = tcp?.Host;
            var port = tcp?.Port;

            if (host == null) throw new InvalidOperationException("No host configured for TCP endpoint.");
            if (port == null) throw new InvalidOperationException("No port configured for TCP endpoint.");
            if (m_config == null) throw new InvalidOperationException("Configuration not initialized.");

            m_logger.Info("TcpAcceptorListener starts.");

            var endPoint = BaseTcpTransport.MakeListenEndPoint(host, port.Value);
            using Socket listener = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            m_logger.Info($"binding to endpoint {endPoint}");
            listener.Bind(endPoint);
            m_logger.Info("listening for new connections.");
            listener.Listen();

            var connections = new List<Task>();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var handle = await listener.AcceptAsync(cancellationToken);

                    // Track the handler task. Previously this used Task.Factory.StartNew with an
                    // async lambda and no Unwrap, so the outer task completed immediately and any
                    // fault inside the connection was swallowed as an unobserved exception.
                    var connection = Task.Run(() => ServeConnection(handle, tcp, cancellationToken), cancellationToken);
                    connections.Add(connection);
                    connections.RemoveAll(t => t.IsCompleted);
                }
            }
            catch (OperationCanceledException)
            {
                m_logger.Info("TcpAcceptorListener accept loop cancelled.");
            }
            finally
            {
                m_logger.Info("TcpAcceptorListener draining {Count} live connection(s).", LiveConnections);
                await Task.WhenAll(connections.Where(t => !t.IsCompleted));
                m_logger.Info("TcpAcceptorListener stopped. TotalConnections={Total}", TotalConnections);
            }
        }

        private async Task ServeConnection(
            Socket handle,
            Types.Config.TcpTransportDescription? tcp,
            CancellationToken cancellationToken)
        {
            var live = Interlocked.Increment(ref _liveConnections);
            var total = Interlocked.Increment(ref _totalConnections);

            // Each connection gets its own parser, encoder and session description. Sharing
            // any of those lets two counterparties interleave bytes into one parse buffer
            // and consume each other's outbound sequence numbers.
            ISessionScope? scope = null;
            ServerSocketTransport? transport = null;

            try
            {
                m_logger.Info(
                    "accepted connection #{Total} from {Remote} - live={Live}",
                    total, handle.RemoteEndPoint, live);

                BaseTcpTransport.ConfigureKeepAlive(handle, tcp, m_logger);

                scope = m_scopeFactory.CreateScope();
                transport = new ServerSocketTransport(handle, scope.Config, m_clock, m_logFactory);
                await transport.AsStream();

                var session = m_sessionFactory.MakeSession(scope);
                m_logger.Info("serving {Remote} with {Scope}", handle.RemoteEndPoint, scope.Id);
                await session.Run(transport, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                m_logger.Info("connection from {Remote} cancelled during shutdown.", SafeRemote(handle));
            }
            catch (Exception ex)
            {
                // A failed connection must not take down the accept loop.
                m_logger.Error(ex, "connection from {Remote} failed: {Message}", SafeRemote(handle), ex.Message);
            }
            finally
            {
                // The socket and its streams were previously never disposed, leaking a
                // handle per connection for the lifetime of the acceptor.
                transport?.Dispose();
                if (transport == null) handle.Dispose();
                scope?.Dispose();

                var remaining = Interlocked.Decrement(ref _liveConnections);
                m_logger.Info("connection closed - live={Live}, total={Total}", remaining, TotalConnections);
            }
        }

        private static string SafeRemote(Socket handle)
        {
            try
            {
                return handle.RemoteEndPoint?.ToString() ?? "unknown";
            }
            catch (ObjectDisposedException)
            {
                return "disposed";
            }
            catch (SocketException)
            {
                return "unknown";
            }
        }
    }
}
