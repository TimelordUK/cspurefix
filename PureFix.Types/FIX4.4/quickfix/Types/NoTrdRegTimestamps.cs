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
		[TagDetails(769)]
		public DateTime? TrdRegTimestamp { get; set; } // UTCTIMESTAMP
		
		[TagDetails(770)]
		public int? TrdRegTimestampType { get; set; } // INT
		
		[TagDetails(771)]
		public string? TrdRegTimestampOrigin { get; set; } // STRING
		
	}
}
