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
		[TagDetails(79)]
		public string? AllocAccount { get; set; } // STRING
		
		[TagDetails(661)]
		public int? AllocAcctIDSource { get; set; } // INT
		
		[TagDetails(736)]
		public string? AllocSettlCurrency { get; set; } // CURRENCY
		
		[TagDetails(467)]
		public string? IndividualAllocID { get; set; } // STRING
		
		public NestedParties? NestedParties { get; set; }
		[TagDetails(80)]
		public double? AllocQty { get; set; } // QTY
		
	}
}
