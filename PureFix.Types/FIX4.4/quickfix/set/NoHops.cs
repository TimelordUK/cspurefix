using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoHops
	{
		public string? HopCompID { get; set; } // 628 STRING
		public DateTime? HopSendingTime { get; set; } // 629 UTCTIMESTAMP
		public int? HopRefID { get; set; } // 630 SEQNUM
	}
}
