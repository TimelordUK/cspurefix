using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AR", FixVersion.FIX44)]
	public sealed partial class TradeCaptureReportAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID {get; set;}
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TradeReportTransType {get; set;}
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TradeReportType {get; set;}
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TrdType {get; set;}
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TrdSubType {get; set;}
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 6, Required = false)]
		public int? SecondaryTrdType {get; set;}
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 7, Required = false)]
		public string? TransferReason {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 8, Required = true)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradeReportRefID {get; set;}
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 10, Required = false)]
		public string? SecondaryTradeReportRefID {get; set;}
		
		[TagDetails(Tag = 939, Type = TagType.Int, Offset = 11, Required = false)]
		public int? TrdRptStatus {get; set;}
		
		[TagDetails(Tag = 751, Type = TagType.Int, Offset = 12, Required = false)]
		public int? TradeReportRejectReason {get; set;}
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecondaryTradeReportID {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradeLinkID {get; set;}
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16, Required = false)]
		public string? TrdMatchID {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 17, Required = false)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecondaryExecID {get; set;}
		
		[Component(Offset = 19, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 20, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public TrdRegTimestampsComponent? TrdRegTimestamps {get; set;}
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 22, Required = false)]
		public int? ResponseTransportType {get; set;}
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 23, Required = false)]
		public string? ResponseDestination {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 27, Required = false)]
		public TrdInstrmtLegGrpComponent? TrdInstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 28, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 29, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 30, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 31, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 32, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 33, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 34, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 35, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 36, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[Component(Offset = 37, Required = false)]
		public TrdAllocGrpComponent? TrdAllocGrp {get; set;}
		
		[Component(Offset = 38, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeReportID is not null
				&& ExecType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (TradeReportTransType is not null) writer.WriteWholeNumber(487, TradeReportTransType.Value);
			if (TradeReportType is not null) writer.WriteWholeNumber(856, TradeReportType.Value);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (SecondaryTrdType is not null) writer.WriteWholeNumber(855, SecondaryTrdType.Value);
			if (TransferReason is not null) writer.WriteString(830, TransferReason);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (TradeReportRefID is not null) writer.WriteString(572, TradeReportRefID);
			if (SecondaryTradeReportRefID is not null) writer.WriteString(881, SecondaryTradeReportRefID);
			if (TrdRptStatus is not null) writer.WriteWholeNumber(939, TrdRptStatus.Value);
			if (TradeReportRejectReason is not null) writer.WriteWholeNumber(751, TradeReportRejectReason.Value);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TradeLinkID is not null) writer.WriteString(820, TradeLinkID);
			if (TrdMatchID is not null) writer.WriteString(880, TrdMatchID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TrdRegTimestamps is not null) ((IFixEncoder)TrdRegTimestamps).Encode(writer);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TrdInstrmtLegGrp is not null) ((IFixEncoder)TrdInstrmtLegGrp).Encode(writer);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (TrdAllocGrp is not null) ((IFixEncoder)TrdAllocGrp).Encode(writer);
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
			TradeReportID = view.GetString(571);
			TradeReportTransType = view.GetInt32(487);
			TradeReportType = view.GetInt32(856);
			TrdType = view.GetInt32(828);
			TrdSubType = view.GetInt32(829);
			SecondaryTrdType = view.GetInt32(855);
			TransferReason = view.GetString(830);
			ExecType = view.GetString(150);
			TradeReportRefID = view.GetString(572);
			SecondaryTradeReportRefID = view.GetString(881);
			TrdRptStatus = view.GetInt32(939);
			TradeReportRejectReason = view.GetInt32(751);
			SecondaryTradeReportID = view.GetString(818);
			SubscriptionRequestType = view.GetString(263);
			TradeLinkID = view.GetString(820);
			TrdMatchID = view.GetString(880);
			ExecID = view.GetString(17);
			SecondaryExecID = view.GetString(527);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			TransactTime = view.GetDateTime(60);
			if (view.GetView("TrdRegTimestamps") is IMessageView viewTrdRegTimestamps)
			{
				TrdRegTimestamps = new();
				((IFixParser)TrdRegTimestamps).Parse(viewTrdRegTimestamps);
			}
			ResponseTransportType = view.GetInt32(725);
			ResponseDestination = view.GetString(726);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("TrdInstrmtLegGrp") is IMessageView viewTrdInstrmtLegGrp)
			{
				TrdInstrmtLegGrp = new();
				((IFixParser)TrdInstrmtLegGrp).Parse(viewTrdInstrmtLegGrp);
			}
			ClearingFeeIndicator = view.GetString(635);
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			CustOrderCapacity = view.GetInt32(582);
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			PositionEffect = view.GetString(77);
			PreallocMethod = view.GetString(591);
			if (view.GetView("TrdAllocGrp") is IMessageView viewTrdAllocGrp)
			{
				TrdAllocGrp = new();
				((IFixParser)TrdAllocGrp).Parse(viewTrdAllocGrp);
			}
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
				case "TradeReportID":
					value = TradeReportID;
					break;
				case "TradeReportTransType":
					value = TradeReportTransType;
					break;
				case "TradeReportType":
					value = TradeReportType;
					break;
				case "TrdType":
					value = TrdType;
					break;
				case "TrdSubType":
					value = TrdSubType;
					break;
				case "SecondaryTrdType":
					value = SecondaryTrdType;
					break;
				case "TransferReason":
					value = TransferReason;
					break;
				case "ExecType":
					value = ExecType;
					break;
				case "TradeReportRefID":
					value = TradeReportRefID;
					break;
				case "SecondaryTradeReportRefID":
					value = SecondaryTradeReportRefID;
					break;
				case "TrdRptStatus":
					value = TrdRptStatus;
					break;
				case "TradeReportRejectReason":
					value = TradeReportRejectReason;
					break;
				case "SecondaryTradeReportID":
					value = SecondaryTradeReportID;
					break;
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
					break;
				case "TradeLinkID":
					value = TradeLinkID;
					break;
				case "TrdMatchID":
					value = TrdMatchID;
					break;
				case "ExecID":
					value = ExecID;
					break;
				case "SecondaryExecID":
					value = SecondaryExecID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "TrdRegTimestamps":
					value = TrdRegTimestamps;
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
				case "TrdInstrmtLegGrp":
					value = TrdInstrmtLegGrp;
					break;
				case "ClearingFeeIndicator":
					value = ClearingFeeIndicator;
					break;
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
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
				case "PositionEffect":
					value = PositionEffect;
					break;
				case "PreallocMethod":
					value = PreallocMethod;
					break;
				case "TrdAllocGrp":
					value = TrdAllocGrp;
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
