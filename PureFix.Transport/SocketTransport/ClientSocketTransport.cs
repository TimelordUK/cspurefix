using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PureFix.Transport.SocketTransport
{
    internal class ClientSocketTransport : BaseTcpTransport
    {
        public ClientSocketTransport(IFixConfig config, IFixClock clock, ILogFactory logFactory) : base(config, clock, logFactory)
        {
            MakeSocket();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            if (m_tcp?.Host == null) throw new InvalidOperationException("No host configured for TCP endpoint.");
            if (m_tcp?.Port == null) throw new InvalidOperationException("No port configured for TCP endpoint.");
            if (m_socket == null) throw new InvalidOperationException("Socket not initialized.");
            if (m_iPEndPoint == null) throw new InvalidOperationException("IPEndPoint not initialized.");

            m_logger?.Info($"connecting to {m_iPEndPoint}");
            await m_socket.ConnectAsync(m_iPEndPoint, cancellationToken);
            m_socket.LingerState = new LingerOption(false, 0);
            await AsStream();
        }
    }
}
