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
    internal class ClientSocketTransport : IMessageTransport, IDisposable
    {
        private Socket? m_socket;
        private readonly TcpTransportDescription? m_tcp;
        private readonly ILogger m_logger;

        public ClientSocketTransport(IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);
            if (config?.Description?.Application?.Tcp == null) throw new InvalidDataException("no config tcp parameters given");
            m_tcp = config?.Description?.Application?.Tcp;
            m_logger = logFactory.MakeLogger("ClientSocketTransport");
        }

        public async Task Connect(CancellationToken cancellationToken)
        {
            if (m_tcp?.Host == null) throw new InvalidDataException("no host for endpoint in tcp config.");
            if (m_tcp?.Port == null) throw new InvalidDataException("no port for endpoint in tcp config.");
            var hostEntry = Dns.GetHostEntry(m_tcp.Host);
            if (hostEntry.AddressList.Length > 0)
            {
                var ipAddress = hostEntry.AddressList[0];
                var ipEndPoint = new IPEndPoint(ipAddress, m_tcp.Port.Value);
                
                m_socket = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                bool connected = false;
                var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));
                while (!cancellationToken.IsCancellationRequested && false == connected)
                {
                    try
                    {
                        m_logger.Info($"connecting to {ipEndPoint}");
                        await m_socket.ConnectAsync(ipEndPoint, cancellationToken);
                        connected = true;
                    }
                    catch (SocketException ex)
                    {
                        m_logger.Error(ex);
                        m_logger.Info($"waiting for re-connecton attempt");
                        await timer.WaitForNextTickAsync(cancellationToken);
                    }
                }       
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
            } else
            {
                throw new InvalidOperationException("no socket to send on.");
            }
        }
    }
}
