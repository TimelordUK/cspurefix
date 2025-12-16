using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Transport.SocketTransport
{
    internal class ServerSocketTransport : BaseTcpTransport
    {
        public ServerSocketTransport(Socket socket, IFixConfig config, IFixClock clock, ILogFactory logFactory) : base(config, clock, logFactory)
        {
            m_socket = socket;   
        }
    }
}
