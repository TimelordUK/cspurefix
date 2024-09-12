using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class BidRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(390)]
		public string? BidID { get; set; } // STRING
		
		[TagDetails(391)]
		public string? ClientBidID { get; set; } // STRING
		
		[TagDetails(374)]
		public string? BidRequestTransType { get; set; } // CHAR
		
		[TagDetails(392)]
		public string? ListName { get; set; } // STRING
		
		[TagDetails(393)]
		public int? TotNoRelatedSym { get; set; } // INT
		
		[TagDetails(394)]
		public int? BidType { get; set; } // INT
		
		[TagDetails(395)]
		public int? NumTickets { get; set; } // INT
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(396)]
		public double? SideValue1 { get; set; } // AMT
		
		[TagDetails(397)]
		public double? SideValue2 { get; set; } // AMT
		
		public BidDescReqGrp? BidDescReqGrp { get; set; }
		public BidCompReqGrp? BidCompReqGrp { get; set; }
		[TagDetails(409)]
		public int? LiquidityIndType { get; set; } // INT
		
		[TagDetails(410)]
		public double? WtAverageLiquidity { get; set; } // PERCENTAGE
		
		[TagDetails(411)]
		public bool? ExchangeForPhysical { get; set; } // BOOLEAN
		
		[TagDetails(412)]
		public double? OutMainCntryUIndex { get; set; } // AMT
		
		[TagDetails(413)]
		public double? CrossPercent { get; set; } // PERCENTAGE
		
		[TagDetails(414)]
		public int? ProgRptReqs { get; set; } // INT
		
		[TagDetails(415)]
		public int? ProgPeriodInterval { get; set; } // INT
		
		[TagDetails(416)]
		public int? IncTaxInd { get; set; } // INT
		
		[TagDetails(121)]
		public bool? ForexReq { get; set; } // BOOLEAN
		
		[TagDetails(417)]
		public int? NumBidders { get; set; } // INT
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(418)]
		public string? BidTradeType { get; set; } // CHAR
		
		[TagDetails(419)]
		public string? BasisPxType { get; set; } // CHAR
		
		[TagDetails(443)]
		public DateTime? StrikeTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
