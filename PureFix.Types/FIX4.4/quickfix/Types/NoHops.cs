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
		[TagDetails(628, TagType.String)]
		public string? HopCompID { get; set; }
		
		[TagDetails(629, TagType.UtcTimestamp)]
		public DateTime? HopSendingTime { get; set; }
		
		[TagDetails(630, TagType.Int)]
		public int? HopRefID { get; set; }
		
	}
}
