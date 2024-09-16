using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AJ", FixVersion.FIX44)]
	public sealed partial class QuoteResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 693, Type = TagType.String, Offset = 1, Required = true)]
		public string? QuoteRespID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = false)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 694, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteRespType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 5, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 6, Required = false)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 7, Required = false)]
		public int? QuoteType { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public QuotQualGrp? QuotQualGrp { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 14, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 15, Required = false)]
		public string? Side { get; set; }
		
		[Component(Offset = 16, Required = false)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 17, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 18, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 19, Required = false)]
		public DateOnly? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 20, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 21, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 23, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 24, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 25, Required = false)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 26, Required = false)]
		public LegQuotGrp? LegQuotGrp { get; set; }
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 27, Required = false)]
		public double? BidPx { get; set; }
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 28, Required = false)]
		public double? OfferPx { get; set; }
		
		[TagDetails(Tag = 645, Type = TagType.Float, Offset = 29, Required = false)]
		public double? MktBidPx { get; set; }
		
		[TagDetails(Tag = 646, Type = TagType.Float, Offset = 30, Required = false)]
		public double? MktOfferPx { get; set; }
		
		[TagDetails(Tag = 647, Type = TagType.Float, Offset = 31, Required = false)]
		public double? MinBidSize { get; set; }
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 32, Required = false)]
		public double? BidSize { get; set; }
		
		[TagDetails(Tag = 648, Type = TagType.Float, Offset = 33, Required = false)]
		public double? MinOfferSize { get; set; }
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 34, Required = false)]
		public double? OfferSize { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 35, Required = false)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 36, Required = false)]
		public double? BidSpotRate { get; set; }
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 37, Required = false)]
		public double? OfferSpotRate { get; set; }
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 38, Required = false)]
		public double? BidForwardPoints { get; set; }
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 39, Required = false)]
		public double? OfferForwardPoints { get; set; }
		
		[TagDetails(Tag = 631, Type = TagType.Float, Offset = 40, Required = false)]
		public double? MidPx { get; set; }
		
		[TagDetails(Tag = 632, Type = TagType.Float, Offset = 41, Required = false)]
		public double? BidYield { get; set; }
		
		[TagDetails(Tag = 633, Type = TagType.Float, Offset = 42, Required = false)]
		public double? MidYield { get; set; }
		
		[TagDetails(Tag = 634, Type = TagType.Float, Offset = 43, Required = false)]
		public double? OfferYield { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 44, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 45, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 642, Type = TagType.Float, Offset = 46, Required = false)]
		public double? BidForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 643, Type = TagType.Float, Offset = 47, Required = false)]
		public double? OfferForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 656, Type = TagType.Float, Offset = 48, Required = false)]
		public double? SettlCurrBidFxRate { get; set; }
		
		[TagDetails(Tag = 657, Type = TagType.Float, Offset = 49, Required = false)]
		public double? SettlCurrOfferFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 50, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 51, Required = false)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 52, Required = false)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 53, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 54, Required = false)]
		public string? ExDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 55, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 56, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 57, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 58, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 59, Required = false)]
		public int? PriceType { get; set; }
		
		[Component(Offset = 60, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 61, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 62, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteRespID is not null
				&& QuoteRespType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteRespID is not null) writer.WriteString(693, QuoteRespID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteRespType is not null) writer.WriteWholeNumber(694, QuoteRespType.Value);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (IOIID is not null) writer.WriteString(23, IOIID);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (QuotQualGrp is not null) ((IFixEncoder)QuotQualGrp).Encode(writer);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (SettlDate2 is not null) writer.WriteLocalDateOnly(193, SettlDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (LegQuotGrp is not null) ((IFixEncoder)LegQuotGrp).Encode(writer);
			if (BidPx is not null) writer.WriteNumber(132, BidPx.Value);
			if (OfferPx is not null) writer.WriteNumber(133, OfferPx.Value);
			if (MktBidPx is not null) writer.WriteNumber(645, MktBidPx.Value);
			if (MktOfferPx is not null) writer.WriteNumber(646, MktOfferPx.Value);
			if (MinBidSize is not null) writer.WriteNumber(647, MinBidSize.Value);
			if (BidSize is not null) writer.WriteNumber(134, BidSize.Value);
			if (MinOfferSize is not null) writer.WriteNumber(648, MinOfferSize.Value);
			if (OfferSize is not null) writer.WriteNumber(135, OfferSize.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (BidSpotRate is not null) writer.WriteNumber(188, BidSpotRate.Value);
			if (OfferSpotRate is not null) writer.WriteNumber(190, OfferSpotRate.Value);
			if (BidForwardPoints is not null) writer.WriteNumber(189, BidForwardPoints.Value);
			if (OfferForwardPoints is not null) writer.WriteNumber(191, OfferForwardPoints.Value);
			if (MidPx is not null) writer.WriteNumber(631, MidPx.Value);
			if (BidYield is not null) writer.WriteNumber(632, BidYield.Value);
			if (MidYield is not null) writer.WriteNumber(633, MidYield.Value);
			if (OfferYield is not null) writer.WriteNumber(634, OfferYield.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (BidForwardPoints2 is not null) writer.WriteNumber(642, BidForwardPoints2.Value);
			if (OfferForwardPoints2 is not null) writer.WriteNumber(643, OfferForwardPoints2.Value);
			if (SettlCurrBidFxRate is not null) writer.WriteNumber(656, SettlCurrBidFxRate.Value);
			if (SettlCurrOfferFxRate is not null) writer.WriteNumber(657, SettlCurrOfferFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (Commission is not null) writer.WriteNumber(12, Commission.Value);
			if (CommType is not null) writer.WriteString(13, CommType);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
