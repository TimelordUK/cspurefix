using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class QuoteStatusReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? QuoteStatusReqID { get; set; } // 649 STRING
		public string? QuoteReqID { get; set; } // 131 STRING
		public string? QuoteID { get; set; } // 117 STRING
		public string? QuoteRespID { get; set; } // 693 STRING
		public int? QuoteType { get; set; } // 537 INT
		public Parties? Parties { get; set; }
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public OrderQtyData? OrderQtyData { get; set; }
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public DateTime? SettlDate2 { get; set; } // 193 LOCALMKTDATE
		public double? OrderQty2 { get; set; } // 192 QTY
		public string? Currency { get; set; } // 15 CURRENCY
		public Stipulations? Stipulations { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public LegQuotStatGrp? LegQuotStatGrp { get; set; }
		public QuotQualGrp? QuotQualGrp { get; set; }
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public double? Price { get; set; } // 44 PRICE
		public int? PriceType { get; set; } // 423 INT
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		public double? BidPx { get; set; } // 132 PRICE
		public double? OfferPx { get; set; } // 133 PRICE
		public double? MktBidPx { get; set; } // 645 PRICE
		public double? MktOfferPx { get; set; } // 646 PRICE
		public double? MinBidSize { get; set; } // 647 QTY
		public double? BidSize { get; set; } // 134 QTY
		public double? MinOfferSize { get; set; } // 648 QTY
		public double? OfferSize { get; set; } // 135 QTY
		public DateTime? ValidUntilTime { get; set; } // 62 UTCTIMESTAMP
		public double? BidSpotRate { get; set; } // 188 PRICE
		public double? OfferSpotRate { get; set; } // 190 PRICE
		public double? BidForwardPoints { get; set; } // 189 PRICEOFFSET
		public double? OfferForwardPoints { get; set; } // 191 PRICEOFFSET
		public double? MidPx { get; set; } // 631 PRICE
		public double? BidYield { get; set; } // 632 PERCENTAGE
		public double? MidYield { get; set; } // 633 PERCENTAGE
		public double? OfferYield { get; set; } // 634 PERCENTAGE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? OrdType { get; set; } // 40 CHAR
		public double? BidForwardPoints2 { get; set; } // 642 PRICEOFFSET
		public double? OfferForwardPoints2 { get; set; } // 643 PRICEOFFSET
		public double? SettlCurrBidFxRate { get; set; } // 656 FLOAT
		public double? SettlCurrOfferFxRate { get; set; } // 657 FLOAT
		public string? SettlCurrFxRateCalc { get; set; } // 156 CHAR
		public string? CommType { get; set; } // 13 CHAR
		public double? Commission { get; set; } // 12 AMT
		public int? CustOrderCapacity { get; set; } // 582 INT
		public string? ExDestination { get; set; } // 100 EXCHANGE
		public int? QuoteStatus { get; set; } // 297 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
