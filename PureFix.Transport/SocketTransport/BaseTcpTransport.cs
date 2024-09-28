using PureFix.Transport.Session;
using PureFix.Types.Config;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace PureFix.Transport.SocketTransport
{
    internal abstract class BaseTcpTransport : IMessageTransport, IDisposable
    {
        protected Socket? m_socket;
        protected IPEndPoint? m_iPEndPoint;
        protected readonly TcpTransportDescription? m_tcp;
        protected readonly ILogger m_logger;

        protected BaseTcpTransport(IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);
          
            if (config?.Description?.Application?.Tcp == null) throw new InvalidDataException("no config tcp parameters given");
            m_tcp = config?.Description?.Application?.Tcp;
            m_logger = logFactory.MakeLogger("BaseTransport");  
        }

        protected void MakeSocket()
        {
            ArgumentNullException.ThrowIfNull(m_tcp);
            ArgumentNullException.ThrowIfNull(m_tcp.Host);
            ArgumentNullException.ThrowIfNull(m_tcp.Port);

            m_iPEndPoint = MakeEndPoint(m_tcp.Host, m_tcp.Port.Value);
            ArgumentNullException.ThrowIfNull(m_iPEndPoint);
            m_socket = new(m_iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp); 
        }

        public static IPEndPoint? MakeEndPoint(string host, int port)
        {
            var hostEntry = Dns.GetHostEntry(host);
            if (hostEntry.AddressList.Length > 0)
            {
                var ipAddress = hostEntry.AddressList[0];
                var iPEndPoint = new IPEndPoint(ipAddress, port);
                return iPEndPoint;
            }
            return null;
        }

        public void Dispose()
        {
            m_socket?.Shutdown(SocketShutdown.Both);
            m_socket = null;
        }

        public async Task<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token)
        {
            if (m_socket != null)
            {
                var received = await m_socket.ReceiveAsync(buffer, SocketFlags.None, token);
                return received;
            }
            else
            {
                throw new InvalidOperationException("no socket to receive on.");
            }
        }

        public async Task SendAsync(ReadOnlyMemory<byte> messageBytes, CancellationToken token)
        {
            if (m_socket != null)
            {
                await m_socket.SendAsync(messageBytes, SocketFlags.None, token);
            }
            else
            {
                throw new InvalidOperationException("no socket to send on.");
            }
        }

      
    }
}
