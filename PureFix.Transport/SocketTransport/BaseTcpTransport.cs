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
            MakeSocket();
        }

        protected void MakeSocket()
        {
            ArgumentNullException.ThrowIfNull(m_tcp);
            ArgumentNullException.ThrowIfNull(m_tcp.Host);
            ArgumentNullException.ThrowIfNull(m_tcp.Port);
            var hostEntry = Dns.GetHostEntry(m_tcp.Host);
            if (hostEntry.AddressList.Length > 0)
            {
                var ipAddress = hostEntry.AddressList[0];
                m_iPEndPoint = new IPEndPoint(ipAddress, m_tcp.Port.Value);
                m_socket = new(m_iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        public void Dispose()
        {
            m_socket?.Shutdown(SocketShutdown.Both);
        }

       
        public async Task<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token)
        {
            if (m_socket != null)
            {
                var received = await m_socket.ReceiveAsync(buffer, SocketFlags.None);
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
                await m_socket.SendAsync(messageBytes, SocketFlags.None);
            }
            else
            {
                throw new InvalidOperationException("no socket to send on.");
            }
        }

        public abstract Task Start(CancellationToken cancellationToken);
    }
}
