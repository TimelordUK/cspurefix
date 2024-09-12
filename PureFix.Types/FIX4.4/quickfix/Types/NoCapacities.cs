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
		public string? OrderCapacity { get; set; } // 528 CHAR
		public string? OrderRestrictions { get; set; } // 529 MULTIPLEVALUESTRING
		public double? OrderCapacityQty { get; set; } // 863 QTY
	}
}
