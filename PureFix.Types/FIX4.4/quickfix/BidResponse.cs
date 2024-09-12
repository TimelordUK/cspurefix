using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("l", FixVersion.FIX44)]
	public sealed class BidResponse : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2)]
		public string? ClientBidID { get; set; }
		
		[Component(Offset = 3)]
		public BidCompRspGrp? BidCompRspGrp { get; set; }
		
		[Component(Offset = 4)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
