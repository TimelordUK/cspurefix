using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class QuoteStatusReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(649)]
		public string? QuoteStatusReqID { get; set; } // STRING
		
		[TagDetails(131)]
		public string? QuoteReqID { get; set; } // STRING
		
		[TagDetails(117)]
		public string? QuoteID { get; set; } // STRING
		
		[TagDetails(693)]
		public string? QuoteRespID { get; set; } // STRING
		
		[TagDetails(537)]
		public int? QuoteType { get; set; } // INT
		
		public Parties? Parties { get; set; }
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		public OrderQtyData? OrderQtyData { get; set; }
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(193)]
		public DateTime? SettlDate2 { get; set; } // LOCALMKTDATE
		
		[TagDetails(192)]
		public double? OrderQty2 { get; set; } // QTY
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		public Stipulations? Stipulations { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		public LegQuotStatGrp? LegQuotStatGrp { get; set; }
		public QuotQualGrp? QuotQualGrp { get; set; }
		[TagDetails(126)]
		public DateTime? ExpireTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(44)]
		public double? Price { get; set; } // PRICE
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		[TagDetails(132)]
		public double? BidPx { get; set; } // PRICE
		
		[TagDetails(133)]
		public double? OfferPx { get; set; } // PRICE
		
		[TagDetails(645)]
		public double? MktBidPx { get; set; } // PRICE
		
		[TagDetails(646)]
		public double? MktOfferPx { get; set; } // PRICE
		
		[TagDetails(647)]
		public double? MinBidSize { get; set; } // QTY
		
		[TagDetails(134)]
		public double? BidSize { get; set; } // QTY
		
		[TagDetails(648)]
		public double? MinOfferSize { get; set; } // QTY
		
		[TagDetails(135)]
		public double? OfferSize { get; set; } // QTY
		
		[TagDetails(62)]
		public DateTime? ValidUntilTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(188)]
		public double? BidSpotRate { get; set; } // PRICE
		
		[TagDetails(190)]
		public double? OfferSpotRate { get; set; } // PRICE
		
		[TagDetails(189)]
		public double? BidForwardPoints { get; set; } // PRICEOFFSET
		
		[TagDetails(191)]
		public double? OfferForwardPoints { get; set; } // PRICEOFFSET
		
		[TagDetails(631)]
		public double? MidPx { get; set; } // PRICE
		
		[TagDetails(632)]
		public double? BidYield { get; set; } // PERCENTAGE
		
		[TagDetails(633)]
		public double? MidYield { get; set; } // PERCENTAGE
		
		[TagDetails(634)]
		public double? OfferYield { get; set; } // PERCENTAGE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(40)]
		public string? OrdType { get; set; } // CHAR
		
		[TagDetails(642)]
		public double? BidForwardPoints2 { get; set; } // PRICEOFFSET
		
		[TagDetails(643)]
		public double? OfferForwardPoints2 { get; set; } // PRICEOFFSET
		
		[TagDetails(656)]
		public double? SettlCurrBidFxRate { get; set; } // FLOAT
		
		[TagDetails(657)]
		public double? SettlCurrOfferFxRate { get; set; } // FLOAT
		
		[TagDetails(156)]
		public string? SettlCurrFxRateCalc { get; set; } // CHAR
		
		[TagDetails(13)]
		public string? CommType { get; set; } // CHAR
		
		[TagDetails(12)]
		public double? Commission { get; set; } // AMT
		
		[TagDetails(582)]
		public int? CustOrderCapacity { get; set; } // INT
		
		[TagDetails(100)]
		public string? ExDestination { get; set; } // EXCHANGE
		
		[TagDetails(297)]
		public int? QuoteStatus { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
