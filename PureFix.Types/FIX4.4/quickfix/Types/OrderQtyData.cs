using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class OrderQtyData
	{
		public double? OrderQty { get; set; } // 38 QTY
		public double? CashOrderQty { get; set; } // 152 QTY
		public double? OrderPercent { get; set; } // 516 PERCENTAGE
		public string? RoundingDirection { get; set; } // 468 CHAR
		public double? RoundingModulus { get; set; } // 469 FLOAT
	}
}
