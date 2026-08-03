using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.Config
{
    public class TcpTransportDescription
    {
        public int? Port { get; set; }
        public string? Host { get; set; }
        public TlsOptions? Tls { get; set; }

        /// <summary>
        /// TCP keep-alive idle time in milliseconds. When set, keep-alive probes detect a
        /// counterparty that vanished without a FIN (half-open socket), which otherwise
        /// leaves the acceptor serving a dead connection until the FIX heartbeat expires.
        /// Null leaves the OS default (keep-alive off).
        /// </summary>
        public int? KeepAliveMs { get; set; }
    }
}
