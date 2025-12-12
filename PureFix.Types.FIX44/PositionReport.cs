using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("AP", FixVersion.FIX44)]
	public sealed partial class PositionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID {get; set;}
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 2, Required = false)]
		public string? PosReqID {get; set;}
		
		[TagDetails(Tag = 724, Type = TagType.Int, Offset = 3, Required = false)]
		public int? PosReqType {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 4, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 727, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TotalNumPosReports {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 728, Type = TagType.Int, Offset = 7, Required = true)]
		public int? PosReqResult {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 8, Required = true)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 9, Required = false)]
		public string? SettlSessID {get; set;}
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 10, Required = false)]
		public string? SettlSessSubID {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public Parties? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 12, Required = true)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 13, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 14, Required = true)]
		public int? AccountType {get; set;}
		
		[Component(Offset = 15, Required = false)]
		public Instrument? Instrument {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 16, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 730, Type = TagType.Float, Offset = 17, Required = true)]
		public double? SettlPrice {get; set;}
		
		[TagDetails(Tag = 731, Type = TagType.Int, Offset = 18, Required = true)]
		public int? SettlPriceType {get; set;}
		
		[TagDetails(Tag = 734, Type = TagType.Float, Offset = 19, Required = true)]
		public double? PriorSettlPrice {get; set;}
		
		[Component(Offset = 20, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public PosUndInstrmtGrp? PosUndInstrmtGrp {get; set;}
		
		[Component(Offset = 22, Required = true)]
		public PositionQty? PositionQty {get; set;}
		
		[Component(Offset = 23, Required = true)]
		public PositionAmountData? PositionAmountData {get; set;}
		
		[TagDetails(Tag = 506, Type = TagType.String, Offset = 24, Required = false)]
		public string? RegistStatus {get; set;}
		
		[TagDetails(Tag = 743, Type = TagType.LocalDate, Offset = 25, Required = false)]
		public DateOnly? DeliveryDate {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 26, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 27, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 28, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 29, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PosMaintRptID is not null) writer.WriteString(721, PosMaintRptID);
			if (PosReqID is not null) writer.WriteString(710, PosReqID);
			if (PosReqType is not null) writer.WriteWholeNumber(724, PosReqType.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TotalNumPosReports is not null) writer.WriteWholeNumber(727, TotalNumPosReports.Value);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (PosReqResult is not null) writer.WriteWholeNumber(728, PosReqResult.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (SettlPrice is not null) writer.WriteNumber(730, SettlPrice.Value);
			if (SettlPriceType is not null) writer.WriteWholeNumber(731, SettlPriceType.Value);
			if (PriorSettlPrice is not null) writer.WriteNumber(734, PriorSettlPrice.Value);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (PosUndInstrmtGrp is not null) ((IFixEncoder)PosUndInstrmtGrp).Encode(writer);
			if (PositionQty is not null) ((IFixEncoder)PositionQty).Encode(writer);
			if (PositionAmountData is not null) ((IFixEncoder)PositionAmountData).Encode(writer);
			if (RegistStatus is not null) writer.WriteString(506, RegistStatus);
			if (DeliveryDate is not null) writer.WriteLocalDateOnly(743, DeliveryDate.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			PosMaintRptID = view.GetString(721);
			PosReqID = view.GetString(710);
			PosReqType = view.GetInt32(724);
			SubscriptionRequestType = view.GetString(263);
			TotalNumPosReports = view.GetInt32(727);
			UnsolicitedIndicator = view.GetBool(325);
			PosReqResult = view.GetInt32(728);
			ClearingBusinessDate = view.GetDateOnly(715);
			SettlSessID = view.GetString(716);
			SettlSessSubID = view.GetString(717);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
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
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			if (view.GetView("PosUndInstrmtGrp") is IMessageView viewPosUndInstrmtGrp)
			{
				PosUndInstrmtGrp = new();
				((IFixParser)PosUndInstrmtGrp).Parse(viewPosUndInstrmtGrp);
			}
			if (view.GetView("PositionQty") is IMessageView viewPositionQty)
			{
				PositionQty = new();
				((IFixParser)PositionQty).Parse(viewPositionQty);
			}
			if (view.GetView("PositionAmountData") is IMessageView viewPositionAmountData)
			{
				PositionAmountData = new();
				((IFixParser)PositionAmountData).Parse(viewPositionAmountData);
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
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
				{
					value = StandardHeader;
					break;
				}
				case "PosMaintRptID":
				{
					value = PosMaintRptID;
					break;
				}
				case "PosReqID":
				{
					value = PosReqID;
					break;
				}
				case "PosReqType":
				{
					value = PosReqType;
					break;
				}
				case "SubscriptionRequestType":
				{
					value = SubscriptionRequestType;
					break;
				}
				case "TotalNumPosReports":
				{
					value = TotalNumPosReports;
					break;
				}
				case "UnsolicitedIndicator":
				{
					value = UnsolicitedIndicator;
					break;
				}
				case "PosReqResult":
				{
					value = PosReqResult;
					break;
				}
				case "ClearingBusinessDate":
				{
					value = ClearingBusinessDate;
					break;
				}
				case "SettlSessID":
				{
					value = SettlSessID;
					break;
				}
				case "SettlSessSubID":
				{
					value = SettlSessSubID;
					break;
				}
				case "Parties":
				{
					value = Parties;
					break;
				}
				case "Account":
				{
					value = Account;
					break;
				}
				case "AcctIDSource":
				{
					value = AcctIDSource;
					break;
				}
				case "AccountType":
				{
					value = AccountType;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "Currency":
				{
					value = Currency;
					break;
				}
				case "SettlPrice":
				{
					value = SettlPrice;
					break;
				}
				case "SettlPriceType":
				{
					value = SettlPriceType;
					break;
				}
				case "PriorSettlPrice":
				{
					value = PriorSettlPrice;
					break;
				}
				case "InstrmtLegGrp":
				{
					value = InstrmtLegGrp;
					break;
				}
				case "PosUndInstrmtGrp":
				{
					value = PosUndInstrmtGrp;
					break;
				}
				case "PositionQty":
				{
					value = PositionQty;
					break;
				}
				case "PositionAmountData":
				{
					value = PositionAmountData;
					break;
				}
				case "RegistStatus":
				{
					value = RegistStatus;
					break;
				}
				case "DeliveryDate":
				{
					value = DeliveryDate;
					break;
				}
				case "Text":
				{
					value = Text;
					break;
				}
				case "EncodedTextLen":
				{
					value = EncodedTextLen;
					break;
				}
				case "EncodedText":
				{
					value = EncodedText;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			PosMaintRptID = null;
			PosReqID = null;
			PosReqType = null;
			SubscriptionRequestType = null;
			TotalNumPosReports = null;
			UnsolicitedIndicator = null;
			PosReqResult = null;
			ClearingBusinessDate = null;
			SettlSessID = null;
			SettlSessSubID = null;
			((IFixReset?)Parties)?.Reset();
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			((IFixReset?)Instrument)?.Reset();
			Currency = null;
			SettlPrice = null;
			SettlPriceType = null;
			PriorSettlPrice = null;
			((IFixReset?)InstrmtLegGrp)?.Reset();
			((IFixReset?)PosUndInstrmtGrp)?.Reset();
			((IFixReset?)PositionQty)?.Reset();
			((IFixReset?)PositionAmountData)?.Reset();
			RegistStatus = null;
			DeliveryDate = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
