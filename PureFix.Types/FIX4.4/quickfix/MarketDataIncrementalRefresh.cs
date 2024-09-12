using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("X", FixVersion.FIX44)]
	public sealed class MarketDataIncrementalRefresh : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1)]
		public string? MDReqID { get; set; }
		
		[Component(Offset = 2)]
		public MDIncGrp? MDIncGrp { get; set; }
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 3)]
		public int? ApplQueueDepth { get; set; }
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 4)]
		public int? ApplQueueResolution { get; set; }
		
		[Component(Offset = 5)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
