using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class OrderQtyData
	{
		public double? OrderQty { get; set; } // 38 QTY
		public double? CashOrderQty { get; set; } // 152 QTY
		public double? OrderPercent { get; set; } // 516 PERCENTAGE
		public string? RoundingDirection { get; set; } // 468 CHAR
		public double? RoundingModulus { get; set; } // 469 FLOAT
	}
}
