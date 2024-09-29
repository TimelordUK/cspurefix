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
        public string? Certificate { get; set; }        
    }
}
