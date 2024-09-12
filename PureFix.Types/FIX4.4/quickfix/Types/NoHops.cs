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
		[TagDetails(628)]
		public string? HopCompID { get; set; } // STRING
		
		[TagDetails(629)]
		public DateTime? HopSendingTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(630)]
		public int? HopRefID { get; set; } // SEQNUM
		
	}
}
