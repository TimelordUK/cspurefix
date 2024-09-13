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
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = true)]
		public string? MDReqID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 2, Required = true)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 264, Type = TagType.Int, Offset = 3, Required = true)]
		public int? MarketDepth { get; set; }
		
		[TagDetails(Tag = 265, Type = TagType.Int, Offset = 4, Required = false)]
		public int? MDUpdateType { get; set; }
		
		[TagDetails(Tag = 266, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? AggregatedBook { get; set; }
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 6, Required = false)]
		public string? OpenCloseSettlFlag { get; set; }
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 7, Required = false)]
		public string? Scope { get; set; }
		
		[TagDetails(Tag = 547, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? MDImplicitDelete { get; set; }
		
		[Component(Offset = 9, Required = true)]
		public MDReqGrp? MDReqGrp { get; set; }
		
		[Component(Offset = 10, Required = true)]
		public InstrmtMDReqGrp? InstrmtMDReqGrp { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 815, Type = TagType.Int, Offset = 12, Required = false)]
		public int? ApplQueueAction { get; set; }
		
		[TagDetails(Tag = 812, Type = TagType.Int, Offset = 13, Required = false)]
		public int? ApplQueueMax { get; set; }
		
		[Component(Offset = 14, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
