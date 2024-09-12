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
		[TagDetails(Tag = 628, Type = TagType.String, Offset = 0)]
		public string? HopCompID { get; set; }
		
		[TagDetails(Tag = 629, Type = TagType.UtcTimestamp, Offset = 1)]
		public DateTime? HopSendingTime { get; set; }
		
		[TagDetails(Tag = 630, Type = TagType.Int, Offset = 2)]
		public int? HopRefID { get; set; }
		
	}
}
