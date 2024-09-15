using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class BidDescReqGrpNoBidDescriptors : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 399, Type = TagType.Int, Offset = 0, Required = false)]
		public int? BidDescriptorType { get; set; }
		
		[TagDetails(Tag = 400, Type = TagType.String, Offset = 1, Required = false)]
		public string? BidDescriptor { get; set; }
		
		[TagDetails(Tag = 401, Type = TagType.Int, Offset = 2, Required = false)]
		public int? SideValueInd { get; set; }
		
		[TagDetails(Tag = 404, Type = TagType.Float, Offset = 3, Required = false)]
		public double? LiquidityValue { get; set; }
		
		[TagDetails(Tag = 441, Type = TagType.Int, Offset = 4, Required = false)]
		public int? LiquidityNumSecurities { get; set; }
		
		[TagDetails(Tag = 402, Type = TagType.Float, Offset = 5, Required = false)]
		public double? LiquidityPctLow { get; set; }
		
		[TagDetails(Tag = 403, Type = TagType.Float, Offset = 6, Required = false)]
		public double? LiquidityPctHigh { get; set; }
		
		[TagDetails(Tag = 405, Type = TagType.Float, Offset = 7, Required = false)]
		public double? EFPTrackingError { get; set; }
		
		[TagDetails(Tag = 406, Type = TagType.Float, Offset = 8, Required = false)]
		public double? FairValue { get; set; }
		
		[TagDetails(Tag = 407, Type = TagType.Float, Offset = 9, Required = false)]
		public double? OutsideIndexPct { get; set; }
		
		[TagDetails(Tag = 408, Type = TagType.Float, Offset = 10, Required = false)]
		public double? ValueOfFutures { get; set; }
		
	}
}
