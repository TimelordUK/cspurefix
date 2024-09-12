using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoTrdRegTimestamps
	{
		[TagDetails(769, TagType.UtcTimestamp)]
		public DateTime? TrdRegTimestamp { get; set; }
		
		[TagDetails(770, TagType.Int)]
		public int? TrdRegTimestampType { get; set; }
		
		[TagDetails(771, TagType.String)]
		public string? TrdRegTimestampOrigin { get; set; }
		
	}
}
