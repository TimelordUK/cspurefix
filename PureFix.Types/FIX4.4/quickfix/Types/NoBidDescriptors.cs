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
		[TagDetails(Tag = 399, Type = TagType.Int, Offset = 0)]
		public int? BidDescriptorType { get; set; }
		
		[TagDetails(Tag = 400, Type = TagType.String, Offset = 1)]
		public string? BidDescriptor { get; set; }
		
		[TagDetails(Tag = 401, Type = TagType.Int, Offset = 2)]
		public int? SideValueInd { get; set; }
		
		[TagDetails(Tag = 404, Type = TagType.Float, Offset = 3)]
		public double? LiquidityValue { get; set; }
		
		[TagDetails(Tag = 441, Type = TagType.Int, Offset = 4)]
		public int? LiquidityNumSecurities { get; set; }
		
		[TagDetails(Tag = 402, Type = TagType.Float, Offset = 5)]
		public double? LiquidityPctLow { get; set; }
		
		[TagDetails(Tag = 403, Type = TagType.Float, Offset = 6)]
		public double? LiquidityPctHigh { get; set; }
		
		[TagDetails(Tag = 405, Type = TagType.Float, Offset = 7)]
		public double? EFPTrackingError { get; set; }
		
		[TagDetails(Tag = 406, Type = TagType.Float, Offset = 8)]
		public double? FairValue { get; set; }
		
		[TagDetails(Tag = 407, Type = TagType.Float, Offset = 9)]
		public double? OutsideIndexPct { get; set; }
		
		[TagDetails(Tag = 408, Type = TagType.Float, Offset = 10)]
		public double? ValueOfFutures { get; set; }
		
	}
}
