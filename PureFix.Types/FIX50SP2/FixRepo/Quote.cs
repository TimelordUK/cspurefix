using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("Quote", FixVersion.FIX50SP2)]
	public sealed partial class Quote : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = true)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 1166, Type = TagType.String, Offset = 3, Required = false)]
		public string? QuoteMsgID {get; set;}
		
		[TagDetails(Tag = 693, Type = TagType.String, Offset = 4, Required = false)]
		public string? QuoteRespID {get; set;}
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteType {get; set;}
		
		[TagDetails(Tag = 1171, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? PrivateQuote {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 7, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 8, Required = false)]
		public QuoteParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 10, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 14, Required = false)]
		public string? Side {get; set;}
		
		[Component(Offset = 15, Required = false)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 16, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 18, Required = false)]
		public DateOnly? SettlDate2 {get; set;}
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 19, Required = false)]
		public double? OrderQty2 {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 20, Required = false)]
		public string? Currency {get; set;}
		
		[Group(NoOfTag = 1019, Offset = 21, Required = false)]
		public QuoteStipulations[]? Stipulations {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 22, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 23, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 24, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 25, Required = false)]
		public double? BidPx {get; set;}
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 26, Required = false)]
		public double? OfferPx {get; set;}
		
		[TagDetails(Tag = 645, Type = TagType.Float, Offset = 27, Required = false)]
		public double? MktBidPx {get; set;}
		
		[TagDetails(Tag = 646, Type = TagType.Float, Offset = 28, Required = false)]
		public double? MktOfferPx {get; set;}
		
		[TagDetails(Tag = 647, Type = TagType.Float, Offset = 29, Required = false)]
		public double? MinBidSize {get; set;}
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 30, Required = false)]
		public double? BidSize {get; set;}
		
		[TagDetails(Tag = 648, Type = TagType.Float, Offset = 31, Required = false)]
		public double? MinOfferSize {get; set;}
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 32, Required = false)]
		public double? OfferSize {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 33, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 34, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 35, Required = false)]
		public double? BidSpotRate {get; set;}
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 36, Required = false)]
		public double? OfferSpotRate {get; set;}
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 37, Required = false)]
		public double? BidForwardPoints {get; set;}
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 38, Required = false)]
		public double? OfferForwardPoints {get; set;}
		
		[TagDetails(Tag = 1065, Type = TagType.Float, Offset = 39, Required = false)]
		public double? BidSwapPoints {get; set;}
		
		[TagDetails(Tag = 1066, Type = TagType.Float, Offset = 40, Required = false)]
		public double? OfferSwapPoints {get; set;}
		
		[TagDetails(Tag = 631, Type = TagType.Float, Offset = 41, Required = false)]
		public double? MidPx {get; set;}
		
		[TagDetails(Tag = 632, Type = TagType.Float, Offset = 42, Required = false)]
		public double? BidYield {get; set;}
		
		[TagDetails(Tag = 633, Type = TagType.Float, Offset = 43, Required = false)]
		public double? MidYield {get; set;}
		
		[TagDetails(Tag = 634, Type = TagType.Float, Offset = 44, Required = false)]
		public double? OfferYield {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 45, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 46, Required = false)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 642, Type = TagType.Float, Offset = 47, Required = false)]
		public double? BidForwardPoints2 {get; set;}
		
		[TagDetails(Tag = 643, Type = TagType.Float, Offset = 48, Required = false)]
		public double? OfferForwardPoints2 {get; set;}
		
		[TagDetails(Tag = 656, Type = TagType.Float, Offset = 49, Required = false)]
		public double? SettlCurrBidFxRate {get; set;}
		
		[TagDetails(Tag = 657, Type = TagType.Float, Offset = 50, Required = false)]
		public double? SettlCurrOfferFxRate {get; set;}
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 51, Required = false)]
		public string? SettlCurrFxRateCalc {get; set;}
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 52, Required = false)]
		public string? CommType {get; set;}
		
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 53, Required = false)]
		public double? Commission {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 54, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 55, Required = false)]
		public string? ExDestination {get; set;}
		
		[TagDetails(Tag = 1133, Type = TagType.String, Offset = 56, Required = false)]
		public string? ExDestinationIDSource {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 57, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 58, Required = false)]
		public int? PriceType {get; set;}
		
		[Component(Offset = 59, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 60, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 61, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 62, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 63, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 64, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 65, Required = false)]
		public int? BookingType {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 66, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 67, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[Group(NoOfTag = 1062, Offset = 68, Required = false)]
		public QuoteRateSource[]? RateSource {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteID is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteMsgID is not null) writer.WriteString(1166, QuoteMsgID);
			if (QuoteRespID is not null) writer.WriteString(693, QuoteRespID);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (PrivateQuote is not null) writer.WriteBoolean(1171, PrivateQuote.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
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
			if (Stipulations is not null && Stipulations.Length != 0)
			{
				writer.WriteWholeNumber(1019, Stipulations.Length);
				for (int i = 0; i < Stipulations.Length; i++)
				{
					((IFixEncoder)Stipulations[i]).Encode(writer);
				}
			}
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (BidPx is not null) writer.WriteNumber(132, BidPx.Value);
			if (OfferPx is not null) writer.WriteNumber(133, OfferPx.Value);
			if (MktBidPx is not null) writer.WriteNumber(645, MktBidPx.Value);
			if (MktOfferPx is not null) writer.WriteNumber(646, MktOfferPx.Value);
			if (MinBidSize is not null) writer.WriteNumber(647, MinBidSize.Value);
			if (BidSize is not null) writer.WriteNumber(134, BidSize.Value);
			if (MinOfferSize is not null) writer.WriteNumber(648, MinOfferSize.Value);
			if (OfferSize is not null) writer.WriteNumber(135, OfferSize.Value);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (BidSpotRate is not null) writer.WriteNumber(188, BidSpotRate.Value);
			if (OfferSpotRate is not null) writer.WriteNumber(190, OfferSpotRate.Value);
			if (BidForwardPoints is not null) writer.WriteNumber(189, BidForwardPoints.Value);
			if (OfferForwardPoints is not null) writer.WriteNumber(191, OfferForwardPoints.Value);
			if (BidSwapPoints is not null) writer.WriteNumber(1065, BidSwapPoints.Value);
			if (OfferSwapPoints is not null) writer.WriteNumber(1066, OfferSwapPoints.Value);
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
			if (CommType is not null) writer.WriteString(13, CommType);
			if (Commission is not null) writer.WriteNumber(12, Commission.Value);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (ExDestinationIDSource is not null) writer.WriteString(1133, ExDestinationIDSource);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (BookingType is not null) writer.WriteWholeNumber(775, BookingType.Value);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (RateSource is not null && RateSource.Length != 0)
			{
				writer.WriteWholeNumber(1062, RateSource.Length);
				for (int i = 0; i < RateSource.Length; i++)
				{
					((IFixEncoder)RateSource[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			QuoteReqID = view.GetString(131);
			QuoteID = view.GetString(117);
			QuoteMsgID = view.GetString(1166);
			QuoteRespID = view.GetString(693);
			QuoteType = view.GetInt32(537);
			PrivateQuote = view.GetBool(1171);
			QuoteResponseLevel = view.GetInt32(301);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new QuoteParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			Side = view.GetString(54);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			SettlType = view.GetString(63);
			SettlDate = view.GetDateOnly(64);
			SettlDate2 = view.GetDateOnly(193);
			OrderQty2 = view.GetDouble(192);
			Currency = view.GetString(15);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				var count = viewStipulations.GroupCount();
				Stipulations = new QuoteStipulations[count];
				for (int i = 0; i < count; i++)
				{
					Stipulations[i] = new();
					((IFixParser)Stipulations[i]).Parse(viewStipulations.GetGroupInstance(i));
				}
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			BidPx = view.GetDouble(132);
			OfferPx = view.GetDouble(133);
			MktBidPx = view.GetDouble(645);
			MktOfferPx = view.GetDouble(646);
			MinBidSize = view.GetDouble(647);
			BidSize = view.GetDouble(134);
			MinOfferSize = view.GetDouble(648);
			OfferSize = view.GetDouble(135);
			MinQty = view.GetDouble(110);
			ValidUntilTime = view.GetDateTime(62);
			BidSpotRate = view.GetDouble(188);
			OfferSpotRate = view.GetDouble(190);
			BidForwardPoints = view.GetDouble(189);
			OfferForwardPoints = view.GetDouble(191);
			BidSwapPoints = view.GetDouble(1065);
			OfferSwapPoints = view.GetDouble(1066);
			MidPx = view.GetDouble(631);
			BidYield = view.GetDouble(632);
			MidYield = view.GetDouble(633);
			OfferYield = view.GetDouble(634);
			TransactTime = view.GetDateTime(60);
			OrdType = view.GetString(40);
			BidForwardPoints2 = view.GetDouble(642);
			OfferForwardPoints2 = view.GetDouble(643);
			SettlCurrBidFxRate = view.GetDouble(656);
			SettlCurrOfferFxRate = view.GetDouble(657);
			SettlCurrFxRateCalc = view.GetString(156);
			CommType = view.GetString(13);
			Commission = view.GetDouble(12);
			CustOrderCapacity = view.GetInt32(582);
			ExDestination = view.GetString(100);
			ExDestinationIDSource = view.GetString(1133);
			OrderCapacity = view.GetString(528);
			PriceType = view.GetInt32(423);
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
			}
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			BookingType = view.GetInt32(775);
			OrderRestrictions = view.GetString(529);
			SettlCurrency = view.GetString(120);
			if (view.GetView("RateSource") is IMessageView viewRateSource)
			{
				var count = viewRateSource.GroupCount();
				RateSource = new QuoteRateSource[count];
				for (int i = 0; i < count; i++)
				{
					RateSource[i] = new();
					((IFixParser)RateSource[i]).Parse(viewRateSource.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "QuoteReqID":
					value = QuoteReqID;
					break;
				case "QuoteID":
					value = QuoteID;
					break;
				case "QuoteMsgID":
					value = QuoteMsgID;
					break;
				case "QuoteRespID":
					value = QuoteRespID;
					break;
				case "QuoteType":
					value = QuoteType;
					break;
				case "PrivateQuote":
					value = PrivateQuote;
					break;
				case "QuoteResponseLevel":
					value = QuoteResponseLevel;
					break;
				case "Parties":
					value = Parties;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "Side":
					value = Side;
					break;
				case "OrderQtyData":
					value = OrderQtyData;
					break;
				case "SettlType":
					value = SettlType;
					break;
				case "SettlDate":
					value = SettlDate;
					break;
				case "SettlDate2":
					value = SettlDate2;
					break;
				case "OrderQty2":
					value = OrderQty2;
					break;
				case "Currency":
					value = Currency;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "Account":
					value = Account;
					break;
				case "AcctIDSource":
					value = AcctIDSource;
					break;
				case "AccountType":
					value = AccountType;
					break;
				case "BidPx":
					value = BidPx;
					break;
				case "OfferPx":
					value = OfferPx;
					break;
				case "MktBidPx":
					value = MktBidPx;
					break;
				case "MktOfferPx":
					value = MktOfferPx;
					break;
				case "MinBidSize":
					value = MinBidSize;
					break;
				case "BidSize":
					value = BidSize;
					break;
				case "MinOfferSize":
					value = MinOfferSize;
					break;
				case "OfferSize":
					value = OfferSize;
					break;
				case "MinQty":
					value = MinQty;
					break;
				case "ValidUntilTime":
					value = ValidUntilTime;
					break;
				case "BidSpotRate":
					value = BidSpotRate;
					break;
				case "OfferSpotRate":
					value = OfferSpotRate;
					break;
				case "BidForwardPoints":
					value = BidForwardPoints;
					break;
				case "OfferForwardPoints":
					value = OfferForwardPoints;
					break;
				case "BidSwapPoints":
					value = BidSwapPoints;
					break;
				case "OfferSwapPoints":
					value = OfferSwapPoints;
					break;
				case "MidPx":
					value = MidPx;
					break;
				case "BidYield":
					value = BidYield;
					break;
				case "MidYield":
					value = MidYield;
					break;
				case "OfferYield":
					value = OfferYield;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "OrdType":
					value = OrdType;
					break;
				case "BidForwardPoints2":
					value = BidForwardPoints2;
					break;
				case "OfferForwardPoints2":
					value = OfferForwardPoints2;
					break;
				case "SettlCurrBidFxRate":
					value = SettlCurrBidFxRate;
					break;
				case "SettlCurrOfferFxRate":
					value = SettlCurrOfferFxRate;
					break;
				case "SettlCurrFxRateCalc":
					value = SettlCurrFxRateCalc;
					break;
				case "CommType":
					value = CommType;
					break;
				case "Commission":
					value = Commission;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
					break;
				case "ExDestination":
					value = ExDestination;
					break;
				case "ExDestinationIDSource":
					value = ExDestinationIDSource;
					break;
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "YieldData":
					value = YieldData;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				case "BookingType":
					value = BookingType;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
					break;
				case "RateSource":
					value = RateSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			QuoteReqID = null;
			QuoteID = null;
			QuoteMsgID = null;
			QuoteRespID = null;
			QuoteType = null;
			PrivateQuote = null;
			QuoteResponseLevel = null;
			Parties = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Side = null;
			((IFixReset?)OrderQtyData)?.Reset();
			SettlType = null;
			SettlDate = null;
			SettlDate2 = null;
			OrderQty2 = null;
			Currency = null;
			Stipulations = null;
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			BidPx = null;
			OfferPx = null;
			MktBidPx = null;
			MktOfferPx = null;
			MinBidSize = null;
			BidSize = null;
			MinOfferSize = null;
			OfferSize = null;
			MinQty = null;
			ValidUntilTime = null;
			BidSpotRate = null;
			OfferSpotRate = null;
			BidForwardPoints = null;
			OfferForwardPoints = null;
			BidSwapPoints = null;
			OfferSwapPoints = null;
			MidPx = null;
			BidYield = null;
			MidYield = null;
			OfferYield = null;
			TransactTime = null;
			OrdType = null;
			BidForwardPoints2 = null;
			OfferForwardPoints2 = null;
			SettlCurrBidFxRate = null;
			SettlCurrOfferFxRate = null;
			SettlCurrFxRateCalc = null;
			CommType = null;
			Commission = null;
			CustOrderCapacity = null;
			ExDestination = null;
			ExDestinationIDSource = null;
			OrderCapacity = null;
			PriceType = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
			BookingType = null;
			OrderRestrictions = null;
			SettlCurrency = null;
			RateSource = null;
		}
	}
}
