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
		public string? BidID { get; set; } // 390 STRING
		public string? ClientBidID { get; set; } // 391 STRING
		public BidCompRspGrp? BidCompRspGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
