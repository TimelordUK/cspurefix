using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public class TlsOptions
    {
        public string? Key { get; set; }
        public string? Cert { get; set; }
        public string[]? ca { get; set; }
        public int? Timeout { get; set; }
        public int? SessionTimeout { get; set; }
        public bool? EnableTrace { get; set; }
        public bool? RequestCert { get; set; }
        public bool? RejectUnauthorized { get; set; }
    }
}
