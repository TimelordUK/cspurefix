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
		public DateTime? TrdRegTimestamp { get; set; } // 769 UTCTIMESTAMP
		public int? TrdRegTimestampType { get; set; } // 770 INT
		public string? TrdRegTimestampOrigin { get; set; } // 771 STRING
	}
}
