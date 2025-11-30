using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.Config
{
    public class TlsOptions
    {
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
        /// Whether to validate the server certificate.
        /// Default is false for self-signed certificates.
        /// </summary>
        public bool ValidateServerCertificate { get; set; } = false;
    }
}
