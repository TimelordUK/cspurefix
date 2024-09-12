using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("W", FixVersion.FIX44)]
	public sealed class MarketDataSnapshotFullRefresh : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1)]
		public string? MDReqID { get; set; }
		
		[Component(Offset = 2)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 3)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 4)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 5)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 6)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 7)]
		public double? NetChgPrevDay { get; set; }
		
		[Component(Offset = 8)]
		public MDFullGrp? MDFullGrp { get; set; }
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 9)]
		public int? ApplQueueDepth { get; set; }
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 10)]
		public int? ApplQueueResolution { get; set; }
		
		[Component(Offset = 11)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
