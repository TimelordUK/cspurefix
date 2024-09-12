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
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(390)]
		public string? BidID { get; set; } // STRING
		
		[TagDetails(391)]
		public string? ClientBidID { get; set; } // STRING
		
		public BidCompRspGrp? BidCompRspGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
