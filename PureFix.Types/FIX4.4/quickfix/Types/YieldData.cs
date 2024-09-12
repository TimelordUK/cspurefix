using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class YieldData
	{
		public string? YieldType { get; set; } // 235 STRING
		public double? Yield { get; set; } // 236 PERCENTAGE
		public DateTime? YieldCalcDate { get; set; } // 701 LOCALMKTDATE
		public DateTime? YieldRedemptionDate { get; set; } // 696 LOCALMKTDATE
		public double? YieldRedemptionPrice { get; set; } // 697 PRICE
		public int? YieldRedemptionPriceType { get; set; } // 698 INT
	}
}
