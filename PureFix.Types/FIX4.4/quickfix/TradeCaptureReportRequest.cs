using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AD", FixVersion.FIX44)]
	public sealed partial class TradeCaptureReportRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeRequestID {get; set;}
		
		[TagDetails(Tag = 569, Type = TagType.Int, Offset = 2, Required = true)]
		public int? TradeRequestType {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradeReportID {get; set;}
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecondaryTradeReportID {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 6, Required = false)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 7, Required = false)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 8, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 9, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 10, Required = false)]
		public string? MatchStatus {get; set;}
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 11, Required = false)]
		public int? TrdType {get; set;}
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 12, Required = false)]
		public int? TrdSubType {get; set;}
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 13, Required = false)]
		public string? TransferReason {get; set;}
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 14, Required = false)]
		public int? SecondaryTrdType {get; set;}
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradeLinkID {get; set;}
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16, Required = false)]
		public string? TrdMatchID {get; set;}
		
		[Component(Offset = 17, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[Component(Offset = 18, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 20, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 22, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[Component(Offset = 23, Required = false)]
		public TrdCapDtGrpComponent? TrdCapDtGrp {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 24, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 25, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 26, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 27, Required = false)]
		public string? TimeBracket {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 28, Required = false)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 29, Required = false)]
		public string? MultiLegReportingType {get; set;}
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 30, Required = false)]
		public string? TradeInputSource {get; set;}
		
		[TagDetails(Tag = 579, Type = TagType.String, Offset = 31, Required = false)]
		public string? TradeInputDevice {get; set;}
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 32, Required = false)]
		public int? ResponseTransportType {get; set;}
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 33, Required = false)]
		public string? ResponseDestination {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 34, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 35, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 36, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 37, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeRequestID is not null
				&& TradeRequestType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeRequestID is not null) writer.WriteString(568, TradeRequestID);
			if (TradeRequestType is not null) writer.WriteWholeNumber(569, TradeRequestType.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (TransferReason is not null) writer.WriteString(830, TransferReason);
			if (SecondaryTrdType is not null) writer.WriteWholeNumber(855, SecondaryTrdType.Value);
			if (TradeLinkID is not null) writer.WriteString(820, TradeLinkID);
			if (TrdMatchID is not null) writer.WriteString(880, TrdMatchID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (TrdCapDtGrp is not null) ((IFixEncoder)TrdCapDtGrp).Encode(writer);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TimeBracket is not null) writer.WriteString(943, TimeBracket);
			if (Side is not null) writer.WriteString(54, Side);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
			if (TradeInputDevice is not null) writer.WriteString(579, TradeInputDevice);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
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
			TradeRequestID = view.GetString(568);
			TradeRequestType = view.GetInt32(569);
			SubscriptionRequestType = view.GetString(263);
			TradeReportID = view.GetString(571);
			SecondaryTradeReportID = view.GetString(818);
			ExecID = view.GetString(17);
			ExecType = view.GetString(150);
			OrderID = view.GetString(37);
			ClOrdID = view.GetString(11);
			MatchStatus = view.GetString(573);
			TrdType = view.GetInt32(828);
			TrdSubType = view.GetInt32(829);
			TransferReason = view.GetString(830);
			SecondaryTrdType = view.GetInt32(855);
			TradeLinkID = view.GetString(820);
			TrdMatchID = view.GetString(880);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("InstrumentExtension") is IMessageView viewInstrumentExtension)
			{
				InstrumentExtension = new();
				((IFixParser)InstrumentExtension).Parse(viewInstrumentExtension);
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
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			if (view.GetView("TrdCapDtGrp") is IMessageView viewTrdCapDtGrp)
			{
				TrdCapDtGrp = new();
				((IFixParser)TrdCapDtGrp).Parse(viewTrdCapDtGrp);
			}
			ClearingBusinessDate = view.GetDateOnly(715);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			TimeBracket = view.GetString(943);
			Side = view.GetString(54);
			MultiLegReportingType = view.GetString(442);
			TradeInputSource = view.GetString(578);
			TradeInputDevice = view.GetString(579);
			ResponseTransportType = view.GetInt32(725);
			ResponseDestination = view.GetString(726);
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
					value = StandardHeader;
					break;
				case "TradeRequestID":
					value = TradeRequestID;
					break;
				case "TradeRequestType":
					value = TradeRequestType;
					break;
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
					break;
				case "TradeReportID":
					value = TradeReportID;
					break;
				case "SecondaryTradeReportID":
					value = SecondaryTradeReportID;
					break;
				case "ExecID":
					value = ExecID;
					break;
				case "ExecType":
					value = ExecType;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "MatchStatus":
					value = MatchStatus;
					break;
				case "TrdType":
					value = TrdType;
					break;
				case "TrdSubType":
					value = TrdSubType;
					break;
				case "TransferReason":
					value = TransferReason;
					break;
				case "SecondaryTrdType":
					value = SecondaryTrdType;
					break;
				case "TradeLinkID":
					value = TradeLinkID;
					break;
				case "TrdMatchID":
					value = TrdMatchID;
					break;
				case "Parties":
					value = Parties;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "InstrumentExtension":
					value = InstrumentExtension;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "TrdCapDtGrp":
					value = TrdCapDtGrp;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "TimeBracket":
					value = TimeBracket;
					break;
				case "Side":
					value = Side;
					break;
				case "MultiLegReportingType":
					value = MultiLegReportingType;
					break;
				case "TradeInputSource":
					value = TradeInputSource;
					break;
				case "TradeInputDevice":
					value = TradeInputDevice;
					break;
				case "ResponseTransportType":
					value = ResponseTransportType;
					break;
				case "ResponseDestination":
					value = ResponseDestination;
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
				default: return false;
			}
			return true;
		}
	}
}
