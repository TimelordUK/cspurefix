using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("TradeCaptureReportAck", FixVersion.FIX50SP2)]
	public sealed partial class TradeCaptureReportAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = false)]
		public string? TradeReportID {get; set;}
		
		[TagDetails(Tag = 1003, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradeID {get; set;}
		
		[TagDetails(Tag = 1040, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryTradeID {get; set;}
		
		[TagDetails(Tag = 1041, Type = TagType.String, Offset = 4, Required = false)]
		public string? FirmTradeID {get; set;}
		
		[TagDetails(Tag = 1042, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecondaryFirmTradeID {get; set;}
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 6, Required = false)]
		public int? TradeReportTransType {get; set;}
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 7, Required = false)]
		public int? TradeReportType {get; set;}
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TrdType {get; set;}
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 9, Required = false)]
		public int? TrdSubType {get; set;}
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 10, Required = false)]
		public int? SecondaryTrdType {get; set;}
		
		[TagDetails(Tag = 1123, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradeHandlingInstr {get; set;}
		
		[TagDetails(Tag = 1124, Type = TagType.String, Offset = 12, Required = false)]
		public string? OrigTradeHandlingInstr {get; set;}
		
		[TagDetails(Tag = 1125, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateOnly? OrigTradeDate {get; set;}
		
		[TagDetails(Tag = 1126, Type = TagType.String, Offset = 14, Required = false)]
		public string? OrigTradeID {get; set;}
		
		[TagDetails(Tag = 1127, Type = TagType.String, Offset = 15, Required = false)]
		public string? OrigSecondaryTradeID {get; set;}
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 16, Required = false)]
		public string? TransferReason {get; set;}
		
		[Group(NoOfTag = 1031, Offset = 17, Required = false)]
		public TradeCaptureReportAckRootParties[]? RootParties {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 18, Required = false)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 19, Required = false)]
		public string? TradeReportRefID {get; set;}
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 20, Required = false)]
		public string? SecondaryTradeReportRefID {get; set;}
		
		[TagDetails(Tag = 939, Type = TagType.Int, Offset = 21, Required = false)]
		public int? TrdRptStatus {get; set;}
		
		[TagDetails(Tag = 751, Type = TagType.Int, Offset = 22, Required = false)]
		public int? TradeReportRejectReason {get; set;}
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 23, Required = false)]
		public string? SecondaryTradeReportID {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 24, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 25, Required = false)]
		public string? TradeLinkID {get; set;}
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 26, Required = false)]
		public string? TrdMatchID {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 27, Required = false)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 28, Required = false)]
		public string? SecondaryExecID {get; set;}
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 29, Required = false)]
		public int? ExecRestatementReason {get; set;}
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 30, Required = false)]
		public bool? PreviouslyReported {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 31, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 822, Type = TagType.String, Offset = 32, Required = false)]
		public string? UnderlyingTradingSessionID {get; set;}
		
		[TagDetails(Tag = 823, Type = TagType.String, Offset = 33, Required = false)]
		public string? UnderlyingTradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 34, Required = false)]
		public string? SettlSessID {get; set;}
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 35, Required = false)]
		public string? SettlSessSubID {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 36, Required = false)]
		public int? QtyType {get; set;}
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 37, Required = false)]
		public double? LastQty {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 38, Required = false)]
		public double? LastPx {get; set;}
		
		[Component(Offset = 39, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 40, Required = false)]
		public double? LastParPx {get; set;}
		
		[TagDetails(Tag = 1056, Type = TagType.Float, Offset = 41, Required = false)]
		public double? CalculatedCcyLastQty {get; set;}
		
		[TagDetails(Tag = 1071, Type = TagType.Float, Offset = 42, Required = false)]
		public double? LastSwapPoints {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 43, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 44, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 45, Required = false)]
		public double? LastSpotRate {get; set;}
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 46, Required = false)]
		public double? LastForwardPoints {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 47, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 48, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 49, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 50, Required = false)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 819, Type = TagType.Int, Offset = 51, Required = false)]
		public int? AvgPxIndicator {get; set;}
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 52, Required = false)]
		public string? MultiLegReportingType {get; set;}
		
		[TagDetails(Tag = 824, Type = TagType.String, Offset = 53, Required = false)]
		public string? TradeLegRefID {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 54, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 55, Required = false)]
		public string? SettlType {get; set;}
		
		[Component(Offset = 56, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 57, Required = false)]
		public string? MatchStatus {get; set;}
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 58, Required = false)]
		public string? MatchType {get; set;}
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 59, Required = false)]
		public bool? CopyMsgIndicator {get; set;}
		
		[TagDetails(Tag = 852, Type = TagType.Boolean, Offset = 60, Required = false)]
		public bool? PublishTrdIndicator {get; set;}
		
		[TagDetails(Tag = 1390, Type = TagType.Int, Offset = 61, Required = false)]
		public int? TradePublishIndicator {get; set;}
		
		[TagDetails(Tag = 853, Type = TagType.Int, Offset = 62, Required = false)]
		public int? ShortSaleReason {get; set;}
		
		[Group(NoOfTag = 1020, Offset = 63, Required = false)]
		public TradeCaptureReportAckTrdRegTimestamps[]? TrdRegTimestamps {get; set;}
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 64, Required = false)]
		public int? ResponseTransportType {get; set;}
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 65, Required = false)]
		public string? ResponseDestination {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 66, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 67, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 68, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 1015, Type = TagType.String, Offset = 69, Required = false)]
		public string? AsOfIndicator {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 70, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[Group(NoOfTag = 1014, Offset = 71, Required = false)]
		public TradeCaptureReportAckPositionAmountData[]? PositionAmountData {get; set;}
		
		[TagDetails(Tag = 994, Type = TagType.String, Offset = 72, Required = false)]
		public string? TierCode {get; set;}
		
		[TagDetails(Tag = 1011, Type = TagType.String, Offset = 73, Required = false)]
		public string? MessageEventSource {get; set;}
		
		[TagDetails(Tag = 779, Type = TagType.UtcTimestamp, Offset = 74, Required = false)]
		public DateTime? LastUpdateTime {get; set;}
		
		[TagDetails(Tag = 991, Type = TagType.Float, Offset = 75, Required = false)]
		public double? RndPx {get; set;}
		
		[TagDetails(Tag = 1135, Type = TagType.String, Offset = 76, Required = false)]
		public string? RptSys {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 77, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 78, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 1329, Type = TagType.Float, Offset = 79, Required = false)]
		public double? FeeMultiplier {get; set;}
		
		[Component(Offset = 80, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[TagDetails(Tag = 1430, Type = TagType.String, Offset = 81, Required = false)]
		public string? VenueType {get; set;}
		
		[TagDetails(Tag = 1300, Type = TagType.String, Offset = 82, Required = false)]
		public string? MarketSegmentID {get; set;}
		
		[TagDetails(Tag = 1301, Type = TagType.String, Offset = 83, Required = false)]
		public string? MarketID {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (TradeID is not null) writer.WriteString(1003, TradeID);
			if (SecondaryTradeID is not null) writer.WriteString(1040, SecondaryTradeID);
			if (FirmTradeID is not null) writer.WriteString(1041, FirmTradeID);
			if (SecondaryFirmTradeID is not null) writer.WriteString(1042, SecondaryFirmTradeID);
			if (TradeReportTransType is not null) writer.WriteWholeNumber(487, TradeReportTransType.Value);
			if (TradeReportType is not null) writer.WriteWholeNumber(856, TradeReportType.Value);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (SecondaryTrdType is not null) writer.WriteWholeNumber(855, SecondaryTrdType.Value);
			if (TradeHandlingInstr is not null) writer.WriteString(1123, TradeHandlingInstr);
			if (OrigTradeHandlingInstr is not null) writer.WriteString(1124, OrigTradeHandlingInstr);
			if (OrigTradeDate is not null) writer.WriteLocalDateOnly(1125, OrigTradeDate.Value);
			if (OrigTradeID is not null) writer.WriteString(1126, OrigTradeID);
			if (OrigSecondaryTradeID is not null) writer.WriteString(1127, OrigSecondaryTradeID);
			if (TransferReason is not null) writer.WriteString(830, TransferReason);
			if (RootParties is not null && RootParties.Length != 0)
			{
				writer.WriteWholeNumber(1031, RootParties.Length);
				for (int i = 0; i < RootParties.Length; i++)
				{
					((IFixEncoder)RootParties[i]).Encode(writer);
				}
			}
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
			if (ExecRestatementReason is not null) writer.WriteWholeNumber(378, ExecRestatementReason.Value);
			if (PreviouslyReported is not null) writer.WriteBoolean(570, PreviouslyReported.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (UnderlyingTradingSessionID is not null) writer.WriteString(822, UnderlyingTradingSessionID);
			if (UnderlyingTradingSessionSubID is not null) writer.WriteString(823, UnderlyingTradingSessionSubID);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (LastParPx is not null) writer.WriteNumber(669, LastParPx.Value);
			if (CalculatedCcyLastQty is not null) writer.WriteNumber(1056, CalculatedCcyLastQty.Value);
			if (LastSwapPoints is not null) writer.WriteNumber(1071, LastSwapPoints.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (AvgPxIndicator is not null) writer.WriteWholeNumber(819, AvgPxIndicator.Value);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (TradeLegRefID is not null) writer.WriteString(824, TradeLegRefID);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (CopyMsgIndicator is not null) writer.WriteBoolean(797, CopyMsgIndicator.Value);
			if (PublishTrdIndicator is not null) writer.WriteBoolean(852, PublishTrdIndicator.Value);
			if (TradePublishIndicator is not null) writer.WriteWholeNumber(1390, TradePublishIndicator.Value);
			if (ShortSaleReason is not null) writer.WriteWholeNumber(853, ShortSaleReason.Value);
			if (TrdRegTimestamps is not null && TrdRegTimestamps.Length != 0)
			{
				writer.WriteWholeNumber(1020, TrdRegTimestamps.Length);
				for (int i = 0; i < TrdRegTimestamps.Length; i++)
				{
					((IFixEncoder)TrdRegTimestamps[i]).Encode(writer);
				}
			}
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (AsOfIndicator is not null) writer.WriteString(1015, AsOfIndicator);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (PositionAmountData is not null && PositionAmountData.Length != 0)
			{
				writer.WriteWholeNumber(1014, PositionAmountData.Length);
				for (int i = 0; i < PositionAmountData.Length; i++)
				{
					((IFixEncoder)PositionAmountData[i]).Encode(writer);
				}
			}
			if (TierCode is not null) writer.WriteString(994, TierCode);
			if (MessageEventSource is not null) writer.WriteString(1011, MessageEventSource);
			if (LastUpdateTime is not null) writer.WriteUtcTimeStamp(779, LastUpdateTime.Value);
			if (RndPx is not null) writer.WriteNumber(991, RndPx.Value);
			if (RptSys is not null) writer.WriteString(1135, RptSys);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (FeeMultiplier is not null) writer.WriteNumber(1329, FeeMultiplier.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (VenueType is not null) writer.WriteString(1430, VenueType);
			if (MarketSegmentID is not null) writer.WriteString(1300, MarketSegmentID);
			if (MarketID is not null) writer.WriteString(1301, MarketID);
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
			TradeID = view.GetString(1003);
			SecondaryTradeID = view.GetString(1040);
			FirmTradeID = view.GetString(1041);
			SecondaryFirmTradeID = view.GetString(1042);
			TradeReportTransType = view.GetInt32(487);
			TradeReportType = view.GetInt32(856);
			TrdType = view.GetInt32(828);
			TrdSubType = view.GetInt32(829);
			SecondaryTrdType = view.GetInt32(855);
			TradeHandlingInstr = view.GetString(1123);
			OrigTradeHandlingInstr = view.GetString(1124);
			OrigTradeDate = view.GetDateOnly(1125);
			OrigTradeID = view.GetString(1126);
			OrigSecondaryTradeID = view.GetString(1127);
			TransferReason = view.GetString(830);
			if (view.GetView("RootParties") is IMessageView viewRootParties)
			{
				var count = viewRootParties.GroupCount();
				RootParties = new TradeCaptureReportAckRootParties[count];
				for (int i = 0; i < count; i++)
				{
					RootParties[i] = new();
					((IFixParser)RootParties[i]).Parse(viewRootParties.GetGroupInstance(i));
				}
			}
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
			ExecRestatementReason = view.GetInt32(378);
			PreviouslyReported = view.GetBool(570);
			PriceType = view.GetInt32(423);
			UnderlyingTradingSessionID = view.GetString(822);
			UnderlyingTradingSessionSubID = view.GetString(823);
			SettlSessID = view.GetString(716);
			SettlSessSubID = view.GetString(717);
			QtyType = view.GetInt32(854);
			LastQty = view.GetDouble(32);
			LastPx = view.GetDouble(31);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			LastParPx = view.GetDouble(669);
			CalculatedCcyLastQty = view.GetDouble(1056);
			LastSwapPoints = view.GetDouble(1071);
			Currency = view.GetString(15);
			SettlCurrency = view.GetString(120);
			LastSpotRate = view.GetDouble(194);
			LastForwardPoints = view.GetDouble(195);
			LastMkt = view.GetString(30);
			TradeDate = view.GetDateOnly(75);
			ClearingBusinessDate = view.GetDateOnly(715);
			AvgPx = view.GetDouble(6);
			AvgPxIndicator = view.GetInt32(819);
			MultiLegReportingType = view.GetString(442);
			TradeLegRefID = view.GetString(824);
			TransactTime = view.GetDateTime(60);
			SettlType = view.GetString(63);
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			MatchStatus = view.GetString(573);
			MatchType = view.GetString(574);
			CopyMsgIndicator = view.GetBool(797);
			PublishTrdIndicator = view.GetBool(852);
			TradePublishIndicator = view.GetInt32(1390);
			ShortSaleReason = view.GetInt32(853);
			if (view.GetView("TrdRegTimestamps") is IMessageView viewTrdRegTimestamps)
			{
				var count = viewTrdRegTimestamps.GroupCount();
				TrdRegTimestamps = new TradeCaptureReportAckTrdRegTimestamps[count];
				for (int i = 0; i < count; i++)
				{
					TrdRegTimestamps[i] = new();
					((IFixParser)TrdRegTimestamps[i]).Parse(viewTrdRegTimestamps.GetGroupInstance(i));
				}
			}
			ResponseTransportType = view.GetInt32(725);
			ResponseDestination = view.GetString(726);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			AsOfIndicator = view.GetString(1015);
			ClearingFeeIndicator = view.GetString(635);
			if (view.GetView("PositionAmountData") is IMessageView viewPositionAmountData)
			{
				var count = viewPositionAmountData.GroupCount();
				PositionAmountData = new TradeCaptureReportAckPositionAmountData[count];
				for (int i = 0; i < count; i++)
				{
					PositionAmountData[i] = new();
					((IFixParser)PositionAmountData[i]).Parse(viewPositionAmountData.GetGroupInstance(i));
				}
			}
			TierCode = view.GetString(994);
			MessageEventSource = view.GetString(1011);
			LastUpdateTime = view.GetDateTime(779);
			RndPx = view.GetDouble(991);
			RptSys = view.GetString(1135);
			GrossTradeAmt = view.GetDouble(381);
			SettlDate = view.GetDateOnly(64);
			FeeMultiplier = view.GetDouble(1329);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			VenueType = view.GetString(1430);
			MarketSegmentID = view.GetString(1300);
			MarketID = view.GetString(1301);
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
				case "TradeID":
					value = TradeID;
					break;
				case "SecondaryTradeID":
					value = SecondaryTradeID;
					break;
				case "FirmTradeID":
					value = FirmTradeID;
					break;
				case "SecondaryFirmTradeID":
					value = SecondaryFirmTradeID;
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
				case "TradeHandlingInstr":
					value = TradeHandlingInstr;
					break;
				case "OrigTradeHandlingInstr":
					value = OrigTradeHandlingInstr;
					break;
				case "OrigTradeDate":
					value = OrigTradeDate;
					break;
				case "OrigTradeID":
					value = OrigTradeID;
					break;
				case "OrigSecondaryTradeID":
					value = OrigSecondaryTradeID;
					break;
				case "TransferReason":
					value = TransferReason;
					break;
				case "RootParties":
					value = RootParties;
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
				case "ExecRestatementReason":
					value = ExecRestatementReason;
					break;
				case "PreviouslyReported":
					value = PreviouslyReported;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "UnderlyingTradingSessionID":
					value = UnderlyingTradingSessionID;
					break;
				case "UnderlyingTradingSessionSubID":
					value = UnderlyingTradingSessionSubID;
					break;
				case "SettlSessID":
					value = SettlSessID;
					break;
				case "SettlSessSubID":
					value = SettlSessSubID;
					break;
				case "QtyType":
					value = QtyType;
					break;
				case "LastQty":
					value = LastQty;
					break;
				case "LastPx":
					value = LastPx;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "LastParPx":
					value = LastParPx;
					break;
				case "CalculatedCcyLastQty":
					value = CalculatedCcyLastQty;
					break;
				case "LastSwapPoints":
					value = LastSwapPoints;
					break;
				case "Currency":
					value = Currency;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
					break;
				case "LastSpotRate":
					value = LastSpotRate;
					break;
				case "LastForwardPoints":
					value = LastForwardPoints;
					break;
				case "LastMkt":
					value = LastMkt;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "AvgPx":
					value = AvgPx;
					break;
				case "AvgPxIndicator":
					value = AvgPxIndicator;
					break;
				case "MultiLegReportingType":
					value = MultiLegReportingType;
					break;
				case "TradeLegRefID":
					value = TradeLegRefID;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "SettlType":
					value = SettlType;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "MatchStatus":
					value = MatchStatus;
					break;
				case "MatchType":
					value = MatchType;
					break;
				case "CopyMsgIndicator":
					value = CopyMsgIndicator;
					break;
				case "PublishTrdIndicator":
					value = PublishTrdIndicator;
					break;
				case "TradePublishIndicator":
					value = TradePublishIndicator;
					break;
				case "ShortSaleReason":
					value = ShortSaleReason;
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
				case "AsOfIndicator":
					value = AsOfIndicator;
					break;
				case "ClearingFeeIndicator":
					value = ClearingFeeIndicator;
					break;
				case "PositionAmountData":
					value = PositionAmountData;
					break;
				case "TierCode":
					value = TierCode;
					break;
				case "MessageEventSource":
					value = MessageEventSource;
					break;
				case "LastUpdateTime":
					value = LastUpdateTime;
					break;
				case "RndPx":
					value = RndPx;
					break;
				case "RptSys":
					value = RptSys;
					break;
				case "GrossTradeAmt":
					value = GrossTradeAmt;
					break;
				case "SettlDate":
					value = SettlDate;
					break;
				case "FeeMultiplier":
					value = FeeMultiplier;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				case "VenueType":
					value = VenueType;
					break;
				case "MarketSegmentID":
					value = MarketSegmentID;
					break;
				case "MarketID":
					value = MarketID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			TradeReportID = null;
			TradeID = null;
			SecondaryTradeID = null;
			FirmTradeID = null;
			SecondaryFirmTradeID = null;
			TradeReportTransType = null;
			TradeReportType = null;
			TrdType = null;
			TrdSubType = null;
			SecondaryTrdType = null;
			TradeHandlingInstr = null;
			OrigTradeHandlingInstr = null;
			OrigTradeDate = null;
			OrigTradeID = null;
			OrigSecondaryTradeID = null;
			TransferReason = null;
			RootParties = null;
			ExecType = null;
			TradeReportRefID = null;
			SecondaryTradeReportRefID = null;
			TrdRptStatus = null;
			TradeReportRejectReason = null;
			SecondaryTradeReportID = null;
			SubscriptionRequestType = null;
			TradeLinkID = null;
			TrdMatchID = null;
			ExecID = null;
			SecondaryExecID = null;
			ExecRestatementReason = null;
			PreviouslyReported = null;
			PriceType = null;
			UnderlyingTradingSessionID = null;
			UnderlyingTradingSessionSubID = null;
			SettlSessID = null;
			SettlSessSubID = null;
			QtyType = null;
			LastQty = null;
			LastPx = null;
			((IFixReset?)Instrument)?.Reset();
			LastParPx = null;
			CalculatedCcyLastQty = null;
			LastSwapPoints = null;
			Currency = null;
			SettlCurrency = null;
			LastSpotRate = null;
			LastForwardPoints = null;
			LastMkt = null;
			TradeDate = null;
			ClearingBusinessDate = null;
			AvgPx = null;
			AvgPxIndicator = null;
			MultiLegReportingType = null;
			TradeLegRefID = null;
			TransactTime = null;
			SettlType = null;
			((IFixReset?)UndInstrmtGrp)?.Reset();
			MatchStatus = null;
			MatchType = null;
			CopyMsgIndicator = null;
			PublishTrdIndicator = null;
			TradePublishIndicator = null;
			ShortSaleReason = null;
			TrdRegTimestamps = null;
			ResponseTransportType = null;
			ResponseDestination = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			AsOfIndicator = null;
			ClearingFeeIndicator = null;
			PositionAmountData = null;
			TierCode = null;
			MessageEventSource = null;
			LastUpdateTime = null;
			RndPx = null;
			RptSys = null;
			GrossTradeAmt = null;
			SettlDate = null;
			FeeMultiplier = null;
			((IFixReset?)StandardTrailer)?.Reset();
			VenueType = null;
			MarketSegmentID = null;
			MarketID = null;
		}
	}
}
