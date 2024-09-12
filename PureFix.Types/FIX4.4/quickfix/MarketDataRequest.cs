using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("V", FixVersion.FIX44)]
	public sealed class MarketDataRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1)]
		public string? MDReqID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 2)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 264, Type = TagType.Int, Offset = 3)]
		public int? MarketDepth { get; set; }
		
		[TagDetails(Tag = 265, Type = TagType.Int, Offset = 4)]
		public int? MDUpdateType { get; set; }
		
		[TagDetails(Tag = 266, Type = TagType.Boolean, Offset = 5)]
		public bool? AggregatedBook { get; set; }
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 6)]
		public string? OpenCloseSettlFlag { get; set; }
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 7)]
		public string? Scope { get; set; }
		
		[TagDetails(Tag = 547, Type = TagType.Boolean, Offset = 8)]
		public bool? MDImplicitDelete { get; set; }
		
		[Component(Offset = 9)]
		public MDReqGrp? MDReqGrp { get; set; }
		
		[Component(Offset = 10)]
		public InstrmtMDReqGrp? InstrmtMDReqGrp { get; set; }
		
		[Component(Offset = 11)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 815, Type = TagType.Int, Offset = 12)]
		public int? ApplQueueAction { get; set; }
		
		[TagDetails(Tag = 812, Type = TagType.Int, Offset = 13)]
		public int? ApplQueueMax { get; set; }
		
		[Component(Offset = 14)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
