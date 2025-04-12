using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("PositionReport", FixVersion.FIX50SP2)]
	public sealed partial class PositionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 2, Required = true)]
		public string? PosMaintRptID {get; set;}
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 3, Required = false)]
		public string? PosReqID {get; set;}
		
		[TagDetails(Tag = 724, Type = TagType.Int, Offset = 4, Required = false)]
		public int? PosReqType {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 5, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 727, Type = TagType.Int, Offset = 6, Required = false)]
		public int? TotalNumPosReports {get; set;}
		
		[TagDetails(Tag = 728, Type = TagType.Int, Offset = 7, Required = false)]
		public int? PosReqResult {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 9, Required = true)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 10, Required = false)]
		public string? SettlSessID {get; set;}
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 11, Required = false)]
		public string? SettlSessSubID {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 12, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 13, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 1011, Type = TagType.String, Offset = 14, Required = false)]
		public string? MessageEventSource {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 15, Required = true)]
		public PositionReportParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 16, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 17, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 18, Required = false)]
		public int? AccountType {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 20, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 730, Type = TagType.Float, Offset = 21, Required = false)]
		public double? SettlPrice {get; set;}
		
		[TagDetails(Tag = 731, Type = TagType.Int, Offset = 22, Required = false)]
		public int? SettlPriceType {get; set;}
		
		[TagDetails(Tag = 734, Type = TagType.Float, Offset = 23, Required = false)]
		public double? PriorSettlPrice {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 24, Required = false)]
		public string? MatchStatus {get; set;}
		
		[Component(Offset = 25, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[Group(NoOfTag = 1015, Offset = 26, Required = false)]
		public PositionReportPositionQty[]? PositionQty {get; set;}
		
		[Group(NoOfTag = 1014, Offset = 27, Required = false)]
		public PositionReportPositionAmountData[]? PositionAmountData {get; set;}
		
		[TagDetails(Tag = 506, Type = TagType.String, Offset = 28, Required = false)]
		public string? RegistStatus {get; set;}
		
		[TagDetails(Tag = 743, Type = TagType.LocalDate, Offset = 29, Required = false)]
		public DateOnly? DeliveryDate {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 30, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 31, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 32, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 33, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[TagDetails(Tag = 1434, Type = TagType.Int, Offset = 34, Required = false)]
		public int? ModelType {get; set;}
		
		[TagDetails(Tag = 811, Type = TagType.Float, Offset = 35, Required = false)]
		public double? PriceDelta {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PosMaintRptID is not null
				&& ClearingBusinessDate is not null
				&& Parties is not null && FixValidator.IsValid(Parties, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (PosMaintRptID is not null) writer.WriteString(721, PosMaintRptID);
			if (PosReqID is not null) writer.WriteString(710, PosReqID);
			if (PosReqType is not null) writer.WriteWholeNumber(724, PosReqType.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TotalNumPosReports is not null) writer.WriteWholeNumber(727, TotalNumPosReports.Value);
			if (PosReqResult is not null) writer.WriteWholeNumber(728, PosReqResult.Value);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (MessageEventSource is not null) writer.WriteString(1011, MessageEventSource);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (SettlPrice is not null) writer.WriteNumber(730, SettlPrice.Value);
			if (SettlPriceType is not null) writer.WriteWholeNumber(731, SettlPriceType.Value);
			if (PriorSettlPrice is not null) writer.WriteNumber(734, PriorSettlPrice.Value);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (PositionQty is not null && PositionQty.Length != 0)
			{
				writer.WriteWholeNumber(1015, PositionQty.Length);
				for (int i = 0; i < PositionQty.Length; i++)
				{
					((IFixEncoder)PositionQty[i]).Encode(writer);
				}
			}
			if (PositionAmountData is not null && PositionAmountData.Length != 0)
			{
				writer.WriteWholeNumber(1014, PositionAmountData.Length);
				for (int i = 0; i < PositionAmountData.Length; i++)
				{
					((IFixEncoder)PositionAmountData[i]).Encode(writer);
				}
			}
			if (RegistStatus is not null) writer.WriteString(506, RegistStatus);
			if (DeliveryDate is not null) writer.WriteLocalDateOnly(743, DeliveryDate.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (ModelType is not null) writer.WriteWholeNumber(1434, ModelType.Value);
			if (PriceDelta is not null) writer.WriteNumber(811, PriceDelta.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			PosMaintRptID = view.GetString(721);
			PosReqID = view.GetString(710);
			PosReqType = view.GetInt32(724);
			SubscriptionRequestType = view.GetString(263);
			TotalNumPosReports = view.GetInt32(727);
			PosReqResult = view.GetInt32(728);
			UnsolicitedIndicator = view.GetBool(325);
			ClearingBusinessDate = view.GetDateOnly(715);
			SettlSessID = view.GetString(716);
			SettlSessSubID = view.GetString(717);
			PriceType = view.GetInt32(423);
			SettlCurrency = view.GetString(120);
			MessageEventSource = view.GetString(1011);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new PositionReportParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			Currency = view.GetString(15);
			SettlPrice = view.GetDouble(730);
			SettlPriceType = view.GetInt32(731);
			PriorSettlPrice = view.GetDouble(734);
			MatchStatus = view.GetString(573);
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			if (view.GetView("PositionQty") is IMessageView viewPositionQty)
			{
				var count = viewPositionQty.GroupCount();
				PositionQty = new PositionReportPositionQty[count];
				for (int i = 0; i < count; i++)
				{
					PositionQty[i] = new();
					((IFixParser)PositionQty[i]).Parse(viewPositionQty.GetGroupInstance(i));
				}
			}
			if (view.GetView("PositionAmountData") is IMessageView viewPositionAmountData)
			{
				var count = viewPositionAmountData.GroupCount();
				PositionAmountData = new PositionReportPositionAmountData[count];
				for (int i = 0; i < count; i++)
				{
					PositionAmountData[i] = new();
					((IFixParser)PositionAmountData[i]).Parse(viewPositionAmountData.GetGroupInstance(i));
				}
			}
			RegistStatus = view.GetString(506);
			DeliveryDate = view.GetDateOnly(743);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			ModelType = view.GetInt32(1434);
			PriceDelta = view.GetDouble(811);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "ApplicationSequenceControl":
					value = ApplicationSequenceControl;
					break;
				case "PosMaintRptID":
					value = PosMaintRptID;
					break;
				case "PosReqID":
					value = PosReqID;
					break;
				case "PosReqType":
					value = PosReqType;
					break;
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
					break;
				case "TotalNumPosReports":
					value = TotalNumPosReports;
					break;
				case "PosReqResult":
					value = PosReqResult;
					break;
				case "UnsolicitedIndicator":
					value = UnsolicitedIndicator;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "SettlSessID":
					value = SettlSessID;
					break;
				case "SettlSessSubID":
					value = SettlSessSubID;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
					break;
				case "MessageEventSource":
					value = MessageEventSource;
					break;
				case "Parties":
					value = Parties;
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
				case "Instrument":
					value = Instrument;
					break;
				case "Currency":
					value = Currency;
					break;
				case "SettlPrice":
					value = SettlPrice;
					break;
				case "SettlPriceType":
					value = SettlPriceType;
					break;
				case "PriorSettlPrice":
					value = PriorSettlPrice;
					break;
				case "MatchStatus":
					value = MatchStatus;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "PositionQty":
					value = PositionQty;
					break;
				case "PositionAmountData":
					value = PositionAmountData;
					break;
				case "RegistStatus":
					value = RegistStatus;
					break;
				case "DeliveryDate":
					value = DeliveryDate;
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
				case "ModelType":
					value = ModelType;
					break;
				case "PriceDelta":
					value = PriceDelta;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			PosMaintRptID = null;
			PosReqID = null;
			PosReqType = null;
			SubscriptionRequestType = null;
			TotalNumPosReports = null;
			PosReqResult = null;
			UnsolicitedIndicator = null;
			ClearingBusinessDate = null;
			SettlSessID = null;
			SettlSessSubID = null;
			PriceType = null;
			SettlCurrency = null;
			MessageEventSource = null;
			Parties = null;
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			((IFixReset?)Instrument)?.Reset();
			Currency = null;
			SettlPrice = null;
			SettlPriceType = null;
			PriorSettlPrice = null;
			MatchStatus = null;
			((IFixReset?)InstrmtLegGrp)?.Reset();
			PositionQty = null;
			PositionAmountData = null;
			RegistStatus = null;
			DeliveryDate = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
			ModelType = null;
			PriceDelta = null;
		}
	}
}
