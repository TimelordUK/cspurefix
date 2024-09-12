using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MarketDataRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(262, TagType.String)]
		public string? MDReqID { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(264, TagType.Int)]
		public int? MarketDepth { get; set; }
		
		[TagDetails(265, TagType.Int)]
		public int? MDUpdateType { get; set; }
		
		[TagDetails(266, TagType.Boolean)]
		public bool? AggregatedBook { get; set; }
		
		[TagDetails(286, TagType.String)]
		public string? OpenCloseSettlFlag { get; set; }
		
		[TagDetails(546, TagType.String)]
		public string? Scope { get; set; }
		
		[TagDetails(547, TagType.Boolean)]
		public bool? MDImplicitDelete { get; set; }
		
		[Component]
		public MDReqGrp? MDReqGrp { get; set; }
		
		[Component]
		public InstrmtMDReqGrp? InstrmtMDReqGrp { get; set; }
		
		[Component]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(815, TagType.Int)]
		public int? ApplQueueAction { get; set; }
		
		[TagDetails(812, TagType.Int)]
		public int? ApplQueueMax { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
