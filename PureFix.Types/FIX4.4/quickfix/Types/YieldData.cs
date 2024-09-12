using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class YieldData
	{
		[TagDetails(235)]
		public string? YieldType { get; set; } // STRING
		
		[TagDetails(236)]
		public double? Yield { get; set; } // PERCENTAGE
		
		[TagDetails(701)]
		public DateTime? YieldCalcDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(696)]
		public DateTime? YieldRedemptionDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(697)]
		public double? YieldRedemptionPrice { get; set; } // PRICE
		
		[TagDetails(698)]
		public int? YieldRedemptionPriceType { get; set; } // INT
		
	}
}
