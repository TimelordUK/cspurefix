using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.Config
{
    public class MsgApplication
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public bool? Resilient { get; set; }
        public int? ReconnectSeconds { get; set; }
        public string? Protocol { get; set; }
        public TcpTransportDescription? Tcp { get; set; }
        public string? Dictionary { get; set; }

        /// <summary>
        /// Delimiter used for human-readable FIX log output.
        /// Defaults to Pipe (|) for readability in tests/debugging.
        /// Set to SOH (0x01) for production logs that can be copy-pasted to test environments.
        /// </summary>
        public byte? LogDelimiter { get; set; } = 0x01;
        public byte? Delimiter { get; set; } = 0x01;

        /// <summary>
        /// Delimiter used when storing messages to the session store.
        /// Defaults to SOH (0x01) for QuickFix compatibility.
        /// </summary>
        public byte? StoreDelimiter { get; set; } = 0x01;
    }
}
