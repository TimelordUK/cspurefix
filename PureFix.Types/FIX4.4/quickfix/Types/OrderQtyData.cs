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
		[TagDetails(38)]
		public double? OrderQty { get; set; } // QTY
		
		[TagDetails(152)]
		public double? CashOrderQty { get; set; } // QTY
		
		[TagDetails(516)]
		public double? OrderPercent { get; set; } // PERCENTAGE
		
		[TagDetails(468)]
		public string? RoundingDirection { get; set; } // CHAR
		
		[TagDetails(469)]
		public double? RoundingModulus { get; set; } // FLOAT
		
	}
}
