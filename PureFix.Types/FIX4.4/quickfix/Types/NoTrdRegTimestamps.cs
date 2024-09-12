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
		[TagDetails(Tag = 769, Type = TagType.UtcTimestamp, Offset = 0, Required = false)]
		public DateTime? TrdRegTimestamp { get; set; }
		
		[TagDetails(Tag = 770, Type = TagType.Int, Offset = 1, Required = false)]
		public int? TrdRegTimestampType { get; set; }
		
		[TagDetails(Tag = 771, Type = TagType.String, Offset = 2, Required = false)]
		public string? TrdRegTimestampOrigin { get; set; }
		
	}
}
