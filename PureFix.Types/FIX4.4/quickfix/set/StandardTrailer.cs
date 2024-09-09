using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX4._4.quickfix.set
{
    public class StandardTrailer
    {
        public int? SignatureLength { get; set; } // [1] 93 (Length)
        public byte[]? Signature { get; set; } // [2] 89 (RawData)
        public string? CheckSum { get; set; } // [3] 10 (String)
    }
}
