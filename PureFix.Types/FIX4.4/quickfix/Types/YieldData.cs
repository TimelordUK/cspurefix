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
		[TagDetails(235, TagType.String)]
		public string? YieldType { get; set; }
		
		[TagDetails(236, TagType.Float)]
		public double? Yield { get; set; }
		
		[TagDetails(701, TagType.LocalDate)]
		public DateTime? YieldCalcDate { get; set; }
		
		[TagDetails(696, TagType.LocalDate)]
		public DateTime? YieldRedemptionDate { get; set; }
		
		[TagDetails(697, TagType.Float)]
		public double? YieldRedemptionPrice { get; set; }
		
		[TagDetails(698, TagType.Int)]
		public int? YieldRedemptionPriceType { get; set; }
		
	}
}
