using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoBidDescriptors
	{
		[TagDetails(399, TagType.Int)]
		public int? BidDescriptorType { get; set; }
		
		[TagDetails(400, TagType.String)]
		public string? BidDescriptor { get; set; }
		
		[TagDetails(401, TagType.Int)]
		public int? SideValueInd { get; set; }
		
		[TagDetails(404, TagType.Float)]
		public double? LiquidityValue { get; set; }
		
		[TagDetails(441, TagType.Int)]
		public int? LiquidityNumSecurities { get; set; }
		
		[TagDetails(402, TagType.Float)]
		public double? LiquidityPctLow { get; set; }
		
		[TagDetails(403, TagType.Float)]
		public double? LiquidityPctHigh { get; set; }
		
		[TagDetails(405, TagType.Float)]
		public double? EFPTrackingError { get; set; }
		
		[TagDetails(406, TagType.Float)]
		public double? FairValue { get; set; }
		
		[TagDetails(407, TagType.Float)]
		public double? OutsideIndexPct { get; set; }
		
		[TagDetails(408, TagType.Float)]
		public double? ValueOfFutures { get; set; }
		
	}
}
