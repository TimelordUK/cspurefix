using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class BidResponse : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(390, TagType.String)]
		public string? BidID { get; set; }
		
		[TagDetails(391, TagType.String)]
		public string? ClientBidID { get; set; }
		
		[Component]
		public BidCompRspGrp? BidCompRspGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
