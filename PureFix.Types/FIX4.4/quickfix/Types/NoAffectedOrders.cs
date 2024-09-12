using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoAffectedOrders
	{
		public string? OrigClOrdID { get; set; } // 41 STRING
		public string? AffectedOrderID { get; set; } // 535 STRING
		public string? AffectedSecondaryOrderID { get; set; } // 536 STRING
	}
}
