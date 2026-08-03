using PureFix.Transport.Session;
using PureFix.Types.Config;
using PureFix.Types;
using System.Diagnostics;
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
        private Stream? m_networkStream;
        private SslStream? m_sslStream;
        private readonly TlsOptions? m_tlsOptions;
        private X509Certificate2Collection? m_trustAnchors;
        private readonly IFixConfig m_config;
        private SslProtocols Protocols { get; } = SslProtocols.Tls12 | SslProtocols.Tls13;

        protected BaseTcpTransport(IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);

            m_config = config;
            m_tcp = config?.Description?.Application?.Tcp ?? throw new InvalidDataException("no config tcp parameters given");
            m_logger = logFactory.MakeLogger("BaseTransport");
            var tls = config?.Description?.Application?.Tcp.Tls;

            // Surface configuration that would not do what it looks like it does - most
            // importantly a fully populated tls block with no "enabled": true, which used
            // to connect in plaintext without a word.
            if (tls != null)
            {
                foreach (var problem in tls.Validate())
                {
                    m_logger.Warn("TLS config: {Problem}", problem);
                }
            }

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
            if (m_socket == null) return;

            m_networkStream = new NetworkStream(m_socket);
            if (m_tlsOptions == null) return;

            try
            {
                await AsSSlStream();
            }
            catch (Exception ex)
            {
                // Fail closed. This used to log and continue, which left m_sslStream null
                // and m_networkStream live, so ReceiveAsync/SendAsync silently fell back to
                // the plaintext socket and sent the Logon credentials in the clear.
                m_logger.Error(ex, "TLS handshake failed - refusing to fall back to plaintext: {Message}", ex.Message);
                m_sslStream?.Dispose();
                m_sslStream = null;
                m_networkStream.Dispose();
                m_networkStream = null;
                throw new AuthenticationException("TLS handshake failed; connection refused.", ex);
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
#if NET9_0_OR_GREATER
                return X509CertificateLoader.LoadPkcs12FromFile(certPath, password);
#else
                return new X509Certificate2(certPath, password);
#endif
            }
            else if (certPath.EndsWith(".pem", StringComparison.OrdinalIgnoreCase))
            {
                // PEM format
#if NET9_0_OR_GREATER
                return X509CertificateLoader.LoadCertificateFromFile(certPath);
#else
                return X509Certificate2.CreateFromPemFile(certPath);
#endif
            }
            else
            {
                // Try PKCS#12 first (most common for client certificates)
#if NET9_0_OR_GREATER
                return X509CertificateLoader.LoadPkcs12FromFile(certPath, password);
#else
                return new X509Certificate2(certPath, password);
#endif
            }
        }

        private async Task AsSSlStream()
        {
            m_logger.Info($"AsSSlStream constructing ssl stream. Protocols={Protocols}");
            ArgumentNullException.ThrowIfNull(m_networkStream);
            ArgumentNullException.ThrowIfNull(m_tlsOptions);

            m_trustAnchors = LoadTrustAnchors(m_tlsOptions.Ca);
            var checkRevocation = m_tlsOptions.CheckCertificateRevocation;

            m_sslStream = new SslStream(m_networkStream, false, ValidatePeerCertificate, null);

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

                await m_sslStream.AuthenticateAsClientAsync(targetHost, certs, Protocols, checkRevocation);
                m_logger.Info("Client authenticated.");
            }
            else
            {
                // Server mode - authenticate clients
                var requestClientCert = m_tlsOptions.RequestClientCertificate || m_tlsOptions.RequireClientCertificate;
                m_logger.Info("Server waiting to authenticate clients. requestClientCert={Request}", requestClientCert);
                var serverCert = MakeCertificate();
                await m_sslStream.AuthenticateAsServerAsync(serverCert, requestClientCert, Protocols, checkRevocation);
                m_logger.Info("Server authenticated.");
            }
        }

        /// <summary>
        /// Loads the configured CA files so a peer chaining to a private root can be accepted
        /// without turning verification off wholesale.
        /// </summary>
        private X509Certificate2Collection? LoadTrustAnchors(List<string>? caPaths)
        {
            if (caPaths is not { Count: > 0 }) return null;

            var anchors = new X509Certificate2Collection();
            foreach (var path in caPaths)
            {
                try
                {
#if NET9_0_OR_GREATER
                    anchors.Add(X509CertificateLoader.LoadCertificateFromFile(path));
#else
                    anchors.Add(X509Certificate2.CreateFromPemFile(path));
#endif
                    m_logger.Info("loaded TLS trust anchor {Path}", path);
                }
                catch (Exception ex)
                {
                    // A trust anchor we cannot read would silently downgrade us to the
                    // machine store, so refuse rather than guess.
                    throw new AuthenticationException($"could not load TLS trust anchor '{path}'", ex);
                }
            }

            return anchors;
        }

        private bool ValidatePeerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            var validate = m_tlsOptions?.ValidateServerCertificate ?? true;

            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            var isServer = !m_config.IsInitiator();
            if (isServer && certificate == null)
            {
                // No client certificate offered. Only fatal when one was required.
                var required = m_tlsOptions?.RequireClientCertificate ?? false;
                if (required) m_logger.Warn("client presented no certificate but requireClientCertificate is set - rejecting");
                return !required;
            }

            if (!validate)
            {
                m_logger.Warn(
                    "accepting peer certificate despite {Errors} because validateServerCertificate is false",
                    sslPolicyErrors);
                return true;
            }

            // A private CA satisfies the chain error only; a name mismatch is still fatal.
            if (m_trustAnchors is { Count: > 0 }
                && sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors
                && certificate != null)
            {
                if (ChainsToConfiguredAnchor(certificate))
                {
                    m_logger.Info("peer certificate accepted via configured trust anchor");
                    return true;
                }
            }

            m_logger.Warn("rejecting peer certificate: {Errors}", sslPolicyErrors);
            return false;
        }

        private bool ChainsToConfiguredAnchor(X509Certificate certificate)
        {
            if (m_trustAnchors == null) return false;

            using var peer = new X509Certificate2(certificate);
            using var chain = new X509Chain();
            chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
            chain.ChainPolicy.RevocationMode = m_tlsOptions?.CheckCertificateRevocation == true
                ? X509RevocationMode.Online
                : X509RevocationMode.NoCheck;
            chain.ChainPolicy.CustomTrustStore.AddRange(m_trustAnchors);

            var built = chain.Build(peer);
            if (!built)
            {
                foreach (var status in chain.ChainStatus)
                {
                    m_logger.Warn("trust anchor chain error: {Status} {Info}", status.Status, status.StatusInformation);
                }
            }

            return built;
        }

        /// <summary>
        /// Resolves a host to an endpoint to connect to. Uses GetHostAddresses rather than
        /// GetHostEntry so an IP literal is not put through a pointless (and often slow or
        /// failing) reverse lookup, and prefers IPv4 for consistency with typical FIX venues.
        /// </summary>
        public static IPEndPoint? MakeEndPoint(string host, int port)
        {
            if (IPAddress.TryParse(host, out var literal))
            {
                return new IPEndPoint(literal, port);
            }

            var addresses = Dns.GetHostAddresses(host);
            if (addresses.Length == 0) return null;

            var preferred = Array.Find(addresses, a => a.AddressFamily == AddressFamily.InterNetwork)
                            ?? addresses[0];
            return new IPEndPoint(preferred, port);
        }

        /// <summary>
        /// Resolves the address an acceptor should bind to. Understands the wildcard forms
        /// ("0.0.0.0", "*", "::", "any") that MakeEndPoint cannot express through DNS.
        /// </summary>
        public static IPEndPoint MakeListenEndPoint(string host, int port)
        {
            switch (host.Trim().ToLowerInvariant())
            {
                case "":
                case "*":
                case "any":
                case "0.0.0.0":
                    return new IPEndPoint(IPAddress.Any, port);
                case "::":
                case "[::]":
                    return new IPEndPoint(IPAddress.IPv6Any, port);
            }

            return MakeEndPoint(host, port)
                   ?? throw new InvalidOperationException($"could not resolve listen address '{host}'");
        }

        /// <summary>
        /// Enables TCP keep-alive so a counterparty that disappears without sending a FIN
        /// is detected at the socket layer rather than being served indefinitely.
        /// </summary>
        public static void ConfigureKeepAlive(Socket socket, TcpTransportDescription? tcp, ILogger? logger = null)
        {
            var keepAliveMs = tcp?.KeepAliveMs;
            if (keepAliveMs is not > 0) return;

            try
            {
                var seconds = Math.Max(1, keepAliveMs.Value / 1000);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime, seconds);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval, seconds);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount, 3);
                logger?.Info("TCP keep-alive enabled: idle={Seconds}s, interval={Seconds}s, retries=3", seconds, seconds);
            }
            catch (SocketException ex)
            {
                // Some platforms reject the per-socket tuning knobs; keep-alive itself may
                // still be on. Not fatal.
                logger?.Warn("could not fully configure TCP keep-alive: {Message}", ex.Message);
            }
            catch (PlatformNotSupportedException ex)
            {
                logger?.Warn("TCP keep-alive tuning not supported on this platform: {Message}", ex.Message);
            }
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
                // Only track bytes received - latency for blocking reads is meaningless
                // as it measures wait time for data, not processing time
                if (received > 0)
                {
                    FixMetrics.BytesReceived.Add(received);
                }
                return received;
            }
            else
            {
                throw new InvalidOperationException("no stream to receive on.");
            }
        }

        public async Task SendAsync(ReadOnlyMemory<byte> messageBytes, CancellationToken token)
        {
            var startTicks = Stopwatch.GetTimestamp();
            var stream = m_sslStream ?? m_networkStream;
            if (stream != null)
            {
                await stream.WriteAsync(messageBytes, token);
                var elapsed = Stopwatch.GetElapsedTime(startTicks);
                FixMetrics.SendLatency.Record(elapsed.TotalMicroseconds);
                FixMetrics.BytesSent.Add(messageBytes.Length);
                FixMetrics.MessagesSent.Add(1);
            }
            else
            {
                throw new InvalidOperationException("no stream to send on.");
            }
        }
    }
}
