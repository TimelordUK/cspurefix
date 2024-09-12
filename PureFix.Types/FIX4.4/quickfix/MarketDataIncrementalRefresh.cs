using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("X")]
	public sealed class MarketDataIncrementalRefresh : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(262, TagType.String)]
		public string? MDReqID { get; set; }
		
		[Component]
		public MDIncGrp? MDIncGrp { get; set; }
		
		[TagDetails(813, TagType.Int)]
		public int? ApplQueueDepth { get; set; }
		
		[TagDetails(814, TagType.Int)]
		public int? ApplQueueResolution { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
