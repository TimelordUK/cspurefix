using PureFix.Transport.Session;
using PureFix.Types.Config;
using PureFix.Types;
using System.Net.Sockets;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace PureFix.Transport.SocketTransport
{
    internal abstract class BaseTcpTransport : IMessageTransport
    {
        protected Socket? m_socket;
        protected IPEndPoint? m_iPEndPoint;
        protected readonly TcpTransportDescription? m_tcp;
        protected readonly ILogger m_logger;
        protected Stream? m_networkStream;
        protected SslStream? m_sslStream;
        protected readonly TlsOptions? m_tlsOptions;
        protected IFixConfig m_config;
        public SslProtocols Protocols { get; set; } = SslProtocols.Tls12 | SslProtocols.Tls13;

        protected BaseTcpTransport(IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);

            m_config = config;
            m_tcp = config?.Description?.Application?.Tcp ?? throw new InvalidDataException("no config tcp parameters given");
            m_logger = logFactory.MakeLogger("BaseTransport");
            var tls = config?.Description?.Application?.Tcp.Tls;
            if (tls?.Enabled is true)
            {
                m_tlsOptions = tls;
            }
        }

        public bool Connected => m_socket is { Connected: true };

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
                    if (m_tlsOptions != null)
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
            ArgumentNullException.ThrowIfNull(m_tlsOptions?.Certificate);
            m_logger.Info($"MakeCertificate {m_tlsOptions.Certificate}");

            // Use the new X509CertificateLoader API (.NET 9+) for loading certificates
            // This replaces the deprecated X509Certificate2 constructor
            var certPath = m_tlsOptions.Certificate;
            var password = m_tlsOptions.Password;

            if (certPath.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase) ||
                certPath.EndsWith(".p12", StringComparison.OrdinalIgnoreCase))
            {
                // PKCS#12 format (typically password-protected)
                return X509CertificateLoader.LoadPkcs12FromFile(certPath, password);
            }
            else if (certPath.EndsWith(".pem", StringComparison.OrdinalIgnoreCase))
            {
                // PEM format
                return X509CertificateLoader.LoadCertificateFromFile(certPath);
            }
            else
            {
                // Try PKCS#12 first (most common for client certificates)
                return X509CertificateLoader.LoadPkcs12FromFile(certPath, password);
            }
        }

        private async Task AsSSlStream()
        {
            m_logger.Info($"AsSSlStream constructing ssl stream. Protocols={Protocols}");
            ArgumentNullException.ThrowIfNull(m_networkStream);
            ArgumentNullException.ThrowIfNull(m_tlsOptions);

            m_sslStream = new SslStream(m_networkStream, false, ValidateServerCertificate, null);

            if (m_config.IsInitiator())
            {
                // Client mode - authenticate to server
                var targetHost = m_tlsOptions.TargetHost ?? m_tcp?.Host ?? "localhost";
                m_logger.Info($"Client authenticating to {targetHost}");

                var certs = new X509Certificate2Collection();
                if (m_tlsOptions.Certificate != null)
                {
                    certs.Add(MakeCertificate());
                }

                await m_sslStream.AuthenticateAsClientAsync(targetHost, certs, Protocols, checkCertificateRevocation: false);
                m_logger.Info("Client authenticated.");
            }
            else
            {
                // Server mode - authenticate clients
                m_logger.Info("Server waiting to authenticate clients.");
                var serverCert = MakeCertificate();
                await m_sslStream.AuthenticateAsServerAsync(serverCert, clientCertificateRequired: false, Protocols, checkCertificateRevocation: false);
                m_logger.Info("Server authenticated.");
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            var validate = m_tlsOptions?.ValidateServerCertificate ?? false;
            m_logger.Info($"ValidateServerCertificate: errors={sslPolicyErrors}, validate={validate}");

            if (!validate)
            {
                // Accept all certificates (useful for self-signed certs in dev/test)
                return true;
            }

            // Only accept if no errors
            return sslPolicyErrors == SslPolicyErrors.None;
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
            m_sslStream?.Dispose();
            m_networkStream?.Dispose();          
            m_socket?.Dispose();
            m_networkStream = null;
            m_socket = null;
            m_sslStream = null;
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
