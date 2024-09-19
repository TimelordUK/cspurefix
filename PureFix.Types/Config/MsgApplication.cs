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
    }
}
