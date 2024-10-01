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
            if (m_tcp?.Host == null) throw new InvalidDataException("no host for endpoint in tcp config.");
            if (m_tcp?.Port == null) throw new InvalidDataException("no port for endpoint in tcp config.");
            if (m_socket == null) throw new InvalidDataException("no socket to connect on.");
            if (m_iPEndPoint == null) throw new InvalidDataException("no iPEndPoint to connect on.");

            await m_socket.ConnectAsync(m_iPEndPoint, cancellationToken);
            m_socket.LingerState = new LingerOption(false, 0);
            await AsStream();
        }
    }
}
