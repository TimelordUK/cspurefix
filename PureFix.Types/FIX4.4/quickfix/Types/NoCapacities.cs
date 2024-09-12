using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoCapacities
	{
		[TagDetails(528)]
		public string? OrderCapacity { get; set; } // CHAR
		
		[TagDetails(529)]
		public string? OrderRestrictions { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(863)]
		public double? OrderCapacityQty { get; set; } // QTY
		
	}
}
