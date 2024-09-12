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
		public string? BidID { get; set; } // 390 STRING
		public string? ClientBidID { get; set; } // 391 STRING
		public string? BidRequestTransType { get; set; } // 374 CHAR
		public string? ListName { get; set; } // 392 STRING
		public int? TotNoRelatedSym { get; set; } // 393 INT
		public int? BidType { get; set; } // 394 INT
		public int? NumTickets { get; set; } // 395 INT
		public string? Currency { get; set; } // 15 CURRENCY
		public double? SideValue1 { get; set; } // 396 AMT
		public double? SideValue2 { get; set; } // 397 AMT
		public BidDescReqGrp? BidDescReqGrp { get; set; }
		public BidCompReqGrp? BidCompReqGrp { get; set; }
		public int? LiquidityIndType { get; set; } // 409 INT
		public double? WtAverageLiquidity { get; set; } // 410 PERCENTAGE
		public bool? ExchangeForPhysical { get; set; } // 411 BOOLEAN
		public double? OutMainCntryUIndex { get; set; } // 412 AMT
		public double? CrossPercent { get; set; } // 413 PERCENTAGE
		public int? ProgRptReqs { get; set; } // 414 INT
		public int? ProgPeriodInterval { get; set; } // 415 INT
		public int? IncTaxInd { get; set; } // 416 INT
		public bool? ForexReq { get; set; } // 121 BOOLEAN
		public int? NumBidders { get; set; } // 417 INT
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public string? BidTradeType { get; set; } // 418 CHAR
		public string? BasisPxType { get; set; } // 419 CHAR
		public DateTime? StrikeTime { get; set; } // 443 UTCTIMESTAMP
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
