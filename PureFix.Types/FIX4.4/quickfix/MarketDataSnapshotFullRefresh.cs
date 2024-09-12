using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MarketDataSnapshotFullRefresh : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(262, TagType.String)]
		public string? MDReqID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(291, TagType.String)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(292, TagType.String)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(451, TagType.Float)]
		public double? NetChgPrevDay { get; set; }
		
		[Component]
		public MDFullGrp? MDFullGrp { get; set; }
		
		[TagDetails(813, TagType.Int)]
		public int? ApplQueueDepth { get; set; }
		
		[TagDetails(814, TagType.Int)]
		public int? ApplQueueResolution { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
