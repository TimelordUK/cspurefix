using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoHops
	{
		public string? HopCompID { get; set; } // 628 STRING
		public DateTime? HopSendingTime { get; set; } // 629 UTCTIMESTAMP
		public int? HopRefID { get; set; } // 630 SEQNUM
	}
}
