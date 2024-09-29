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
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace PureFix.Transport.SocketTransport
{
    internal abstract class BaseTcpTransport : IMessageTransport, IDisposable
    {
        protected Socket? m_socket;
        protected IPEndPoint? m_iPEndPoint;
        protected readonly TcpTransportDescription? m_tcp;
        protected readonly ILogger m_logger;
        protected Stream? m_networkStream;
        protected SslStream? m_sslStream;
        protected string? m_sslCertificate;
        protected IFixConfig m_config;

        protected BaseTcpTransport(IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);
          
            if (config?.Description?.Application?.Tcp == null) throw new InvalidDataException("no config tcp parameters given");
            m_config = config;
            m_tcp = config?.Description?.Application?.Tcp;
            m_logger = logFactory.MakeLogger("BaseTransport");
            var tls = config?.Description?.Application?.Tcp.Tls;
            if (tls != null)
            {
                if (tls.Enabled != false && tls.Enabled == true) {
                    m_sslCertificate = tls.Certificate;
                }
            }
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

        public async Task AsStream()
        {
            if (m_socket != null)
            {
                try
                {
                    m_networkStream = new NetworkStream(m_socket);
                    if (m_sslCertificate != null)
                    {
                        await AsSSlStream();
                    }
                }
                catch (Exception ex)
                {
                    m_logger.Error(ex);
                }
            }
        }

        private X509Certificate2 MakeCertificate()
        {
            ArgumentNullException.ThrowIfNull(m_sslCertificate);
            return new X509Certificate2(m_sslCertificate, string.Empty); 
        }

        private async Task AsSSlStream()
        {
            ArgumentNullException.ThrowIfNull(m_networkStream);
            ArgumentNullException.ThrowIfNull(m_sslCertificate);
            if (m_config.IsInitiator())
            {
                m_sslStream = new SslStream(m_networkStream, false, ValidateServerCertificate, null);
                var certs = new X509Certificate2Collection
                        {
                            MakeCertificate()
                        };
                await m_sslStream.AuthenticateAsClientAsync("localhost", certs, SslProtocols.Tls12, false);
            }
            else
            {
                m_sslStream = new SslStream(m_networkStream, false, ValidateServerCertificate, null);
                m_sslStream.AuthenticateAsServer(MakeCertificate(), false, SslProtocols.Tls12, false);
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            // For self-signed certificate, always return true.
            m_logger.Info("ValidateServerCertificate");
            return true;
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
            m_networkStream?.Dispose();
            m_networkStream = null;
            m_socket?.Dispose();
            m_socket = null;
        }

        public async Task<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token)
        {
            var stream = m_sslStream ?? m_networkStream;
            if (stream != null)
            {
                var received = await stream.ReadAsync(buffer, token);
                return received;
            }
            else
            {
                throw new InvalidOperationException("no stream to receive on.");
            }
        }

        public async Task SendAsync(ReadOnlyMemory<byte> messageBytes, CancellationToken token)
        {
            var stream = m_sslStream ?? m_networkStream;
            if (stream != null)
            {
                await stream.WriteAsync(messageBytes, token);
            }
            else
            {
                throw new InvalidOperationException("no stream to send on.");
            }
        }   
    }
}
