using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX4._4.quickfix.set
{
    public class HopNoHops
    {
        public string?  HopCompID { get; set; }// [1] 628 (String)
        public DateTime? HopSendingTime { get; set; } // [2] 629 (UtcTimestamp)
        public int? HopRefID { get; set; } // [3] 630 (Int)
    }
}
