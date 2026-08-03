using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PureFix.Types.Config
{
    public class TlsOptions
    {
        /// <summary>
        /// Master switch. Null or false means the whole tls block is inert - see
        /// <see cref="Validate"/>, which reports a populated-but-disabled block rather than
        /// letting it silently connect in plaintext.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Path to the certificate file (.pfx, .p12, or .pem)
        /// </summary>
        public string? Certificate { get; set; }

        /// <summary>
        /// Password for the certificate file (if password-protected)
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Target hostname for SNI (Server Name Indication).
        /// If not set, uses the Host from TcpTransportDescription.
        /// </summary>
        public string? TargetHost { get; set; }

        /// <summary>
        /// Whether to validate the peer certificate chain. Defaults to true.
        /// </summary>
        /// <remarks>
        /// Set false only to accept self-signed certificates in dev/test. To trust a
        /// private CA in production keep this true and list the CA in <see cref="Ca"/>,
        /// which pins trust to that root instead of disabling verification entirely.
        /// </remarks>
        public bool ValidateServerCertificate { get; set; } = true;

        /// <summary>
        /// Paths to additional PEM/CRT trust anchors. A peer certificate that chains to one
        /// of these is accepted even when it is not in the machine trust store.
        /// </summary>
        public List<string>? Ca { get; set; }

        /// <summary>
        /// Acceptor only: ask the client for a certificate during the handshake.
        /// </summary>
        public bool RequestClientCertificate { get; set; } = false;

        /// <summary>
        /// Acceptor only: reject a client that presents no certificate, or one that fails
        /// validation. Implies <see cref="RequestClientCertificate"/>.
        /// </summary>
        public bool RequireClientCertificate { get; set; } = false;

        /// <summary>
        /// Whether to check certificate revocation during the handshake. Defaults to false
        /// because many FIX venues use private CAs without reachable CRL/OCSP endpoints.
        /// </summary>
        public bool CheckCertificateRevocation { get; set; } = false;

        /// <summary>
        /// Any JSON keys that did not map to a property above. Captured so configuration
        /// written against a different engine's spelling (jspurefix uses key/cert/ca/timeout)
        /// is reported instead of being dropped on the floor.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? UnknownOptions { get; set; }

        /// <summary>
        /// Returns messages describing configuration that would not behave as written.
        /// Empty when the block is coherent.
        /// </summary>
        public IReadOnlyList<string> Validate()
        {
            var problems = new List<string>();

            var populated = Certificate != null
                            || Password != null
                            || TargetHost != null
                            || Ca is { Count: > 0 }
                            || UnknownOptions is { Count: > 0 };

            if (Enabled != true && populated)
            {
                problems.Add(
                    "tls block is populated but 'enabled' is not true - the connection will be PLAINTEXT. " +
                    "Add \"enabled\": true, or remove the tls block.");
            }

            if (UnknownOptions is { Count: > 0 })
            {
                var keys = string.Join(", ", UnknownOptions.Keys);
                problems.Add(
                    $"unrecognised tls option(s) ignored: {keys}. " +
                    "Supported: enabled, certificate, password, targetHost, validateServerCertificate, " +
                    "ca, requestClientCertificate, requireClientCertificate, checkCertificateRevocation.");
            }

            if (Enabled == true && !ValidateServerCertificate)
            {
                problems.Add(
                    "validateServerCertificate is false - the peer certificate is NOT verified. " +
                    "Acceptable for dev/test only; in production list your CA under 'ca' instead.");
            }

            return problems;
        }
    }
}
