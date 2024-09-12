using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoAllocs
	{
		public string? AllocAccount { get; set; } // 79 STRING
		public int? AllocAcctIDSource { get; set; } // 661 INT
		public string? AllocSettlCurrency { get; set; } // 736 CURRENCY
		public string? IndividualAllocID { get; set; } // 467 STRING
		public NestedParties? NestedParties { get; set; }
		public double? AllocQty { get; set; } // 80 QTY
	}
}
