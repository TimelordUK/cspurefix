using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest
{
	[MessageType("AE", FixVersion.FIX44)]
	public sealed partial class TradeCaptureReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID {get; set;}
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TradeReportTransType {get; set;}
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TradeReportType {get; set;}
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradeRequestID {get; set;}
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TrdType {get; set;}
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 6, Required = false)]
		public int? TrdSubType {get; set;}
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 7, Required = false)]
		public int? SecondaryTrdType {get; set;}
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 8, Required = false)]
		public string? TransferReason {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 9, Required = false)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 748, Type = TagType.Int, Offset = 10, Required = false)]
		public int? TotNumTradeReports {get; set;}
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 11, Required = false)]
		public bool? LastRptRequested {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 12, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 13, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 14, Required = false)]
		public string? TradeReportRefID {get; set;}
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecondaryTradeReportRefID {get; set;}
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 16, Required = false)]
		public string? SecondaryTradeReportID {get; set;}
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 17, Required = false)]
		public string? TradeLinkID {get; set;}
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 18, Required = false)]
		public string? TrdMatchID {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 19, Required = false)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 20, Required = false)]
		public string? OrdStatus {get; set;}
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 21, Required = false)]
		public string? SecondaryExecID {get; set;}
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 22, Required = false)]
		public int? ExecRestatementReason {get; set;}
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 23, Required = true)]
		public bool? PreviouslyReported {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 24, Required = false)]
		public int? PriceType {get; set;}
		
		[Component(Offset = 25, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[Component(Offset = 26, Required = false)]
		public FinancingDetails? FinancingDetails {get; set;}
		
		[Component(Offset = 27, Required = false)]
		public OrderQtyData? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 28, Required = false)]
		public int? QtyType {get; set;}
		
		[Component(Offset = 29, Required = false)]
		public YieldData? YieldData {get; set;}
		
		[Component(Offset = 30, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 822, Type = TagType.String, Offset = 31, Required = false)]
		public string? UnderlyingTradingSessionID {get; set;}
		
		[TagDetails(Tag = 823, Type = TagType.String, Offset = 32, Required = false)]
		public string? UnderlyingTradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 33, Required = true)]
		public double? LastQty {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 34, Required = true)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 35, Required = false)]
		public double? LastParPx {get; set;}
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 36, Required = false)]
		public double? LastSpotRate {get; set;}
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 37, Required = false)]
		public double? LastForwardPoints {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 38, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 39, Required = true)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 40, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 41, Required = false)]
		public double? AvgPx {get; set;}
		
		[Component(Offset = 42, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData {get; set;}
		
		[TagDetails(Tag = 819, Type = TagType.Int, Offset = 43, Required = false)]
		public int? AvgPxIndicator {get; set;}
		
		[Component(Offset = 44, Required = false)]
		public PositionAmountData? PositionAmountData {get; set;}
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 45, Required = false)]
		public string? MultiLegReportingType {get; set;}
		
		[TagDetails(Tag = 824, Type = TagType.String, Offset = 46, Required = false)]
		public string? TradeLegRefID {get; set;}
		
		[Component(Offset = 47, Required = false)]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 48, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 49, Required = false)]
		public TrdRegTimestamps? TrdRegTimestamps {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 50, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 51, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 52, Required = false)]
		public string? MatchStatus {get; set;}
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 53, Required = false)]
		public string? MatchType {get; set;}
		
		[Component(Offset = 54, Required = true)]
		public TrdCapRptSideGrp? TrdCapRptSideGrp {get; set;}
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 55, Required = false)]
		public bool? CopyMsgIndicator {get; set;}
		
		[TagDetails(Tag = 852, Type = TagType.Boolean, Offset = 56, Required = false)]
		public bool? PublishTrdIndicator {get; set;}
		
		[TagDetails(Tag = 853, Type = TagType.Int, Offset = 57, Required = false)]
		public int? ShortSaleReason {get; set;}
		
		[Component(Offset = 58, Required = true)]
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
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (TradeReportTransType is not null) writer.WriteWholeNumber(487, TradeReportTransType.Value);
			if (TradeReportType is not null) writer.WriteWholeNumber(856, TradeReportType.Value);
			if (TradeRequestID is not null) writer.WriteString(568, TradeRequestID);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (SecondaryTrdType is not null) writer.WriteWholeNumber(855, SecondaryTrdType.Value);
			if (TransferReason is not null) writer.WriteString(830, TransferReason);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (TotNumTradeReports is not null) writer.WriteWholeNumber(748, TotNumTradeReports.Value);
			if (LastRptRequested is not null) writer.WriteBoolean(912, LastRptRequested.Value);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TradeReportRefID is not null) writer.WriteString(572, TradeReportRefID);
			if (SecondaryTradeReportRefID is not null) writer.WriteString(881, SecondaryTradeReportRefID);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
			if (TradeLinkID is not null) writer.WriteString(820, TradeLinkID);
			if (TrdMatchID is not null) writer.WriteString(880, TrdMatchID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (ExecRestatementReason is not null) writer.WriteWholeNumber(378, ExecRestatementReason.Value);
			if (PreviouslyReported is not null) writer.WriteBoolean(570, PreviouslyReported.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (UnderlyingTradingSessionID is not null) writer.WriteString(822, UnderlyingTradingSessionID);
			if (UnderlyingTradingSessionSubID is not null) writer.WriteString(823, UnderlyingTradingSessionSubID);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (LastParPx is not null) writer.WriteNumber(669, LastParPx.Value);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (AvgPxIndicator is not null) writer.WriteWholeNumber(819, AvgPxIndicator.Value);
			if (PositionAmountData is not null) ((IFixEncoder)PositionAmountData).Encode(writer);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (TradeLegRefID is not null) writer.WriteString(824, TradeLegRefID);
			if (TrdInstrmtLegGrp is not null) ((IFixEncoder)TrdInstrmtLegGrp).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TrdRegTimestamps is not null) ((IFixEncoder)TrdRegTimestamps).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (TrdCapRptSideGrp is not null) ((IFixEncoder)TrdCapRptSideGrp).Encode(writer);
			if (CopyMsgIndicator is not null) writer.WriteBoolean(797, CopyMsgIndicator.Value);
			if (PublishTrdIndicator is not null) writer.WriteBoolean(852, PublishTrdIndicator.Value);
			if (ShortSaleReason is not null) writer.WriteWholeNumber(853, ShortSaleReason.Value);
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
			TradeRequestID = view.GetString(568);
			TrdType = view.GetInt32(828);
			TrdSubType = view.GetInt32(829);
			SecondaryTrdType = view.GetInt32(855);
			TransferReason = view.GetString(830);
			ExecType = view.GetString(150);
			TotNumTradeReports = view.GetInt32(748);
			LastRptRequested = view.GetBool(912);
			UnsolicitedIndicator = view.GetBool(325);
			SubscriptionRequestType = view.GetString(263);
			TradeReportRefID = view.GetString(572);
			SecondaryTradeReportRefID = view.GetString(881);
			SecondaryTradeReportID = view.GetString(818);
			TradeLinkID = view.GetString(820);
			TrdMatchID = view.GetString(880);
			ExecID = view.GetString(17);
			OrdStatus = view.GetString(39);
			SecondaryExecID = view.GetString(527);
			ExecRestatementReason = view.GetInt32(378);
			PreviouslyReported = view.GetBool(570);
			PriceType = view.GetInt32(423);
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
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			QtyType = view.GetInt32(854);
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			UnderlyingTradingSessionID = view.GetString(822);
			UnderlyingTradingSessionSubID = view.GetString(823);
			LastQty = view.GetDouble(32);
			LastPx = view.GetDouble(31);
			LastParPx = view.GetDouble(669);
			LastSpotRate = view.GetDouble(194);
			LastForwardPoints = view.GetDouble(195);
			LastMkt = view.GetString(30);
			TradeDate = view.GetDateOnly(75);
			ClearingBusinessDate = view.GetDateOnly(715);
			AvgPx = view.GetDouble(6);
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			AvgPxIndicator = view.GetInt32(819);
			if (view.GetView("PositionAmountData") is IMessageView viewPositionAmountData)
			{
				PositionAmountData = new();
				((IFixParser)PositionAmountData).Parse(viewPositionAmountData);
			}
			MultiLegReportingType = view.GetString(442);
			TradeLegRefID = view.GetString(824);
			if (view.GetView("TrdInstrmtLegGrp") is IMessageView viewTrdInstrmtLegGrp)
			{
				TrdInstrmtLegGrp = new();
				((IFixParser)TrdInstrmtLegGrp).Parse(viewTrdInstrmtLegGrp);
			}
			TransactTime = view.GetDateTime(60);
			if (view.GetView("TrdRegTimestamps") is IMessageView viewTrdRegTimestamps)
			{
				TrdRegTimestamps = new();
				((IFixParser)TrdRegTimestamps).Parse(viewTrdRegTimestamps);
			}
			SettlType = view.GetString(63);
			SettlDate = view.GetDateOnly(64);
			MatchStatus = view.GetString(573);
			MatchType = view.GetString(574);
			if (view.GetView("TrdCapRptSideGrp") is IMessageView viewTrdCapRptSideGrp)
			{
				TrdCapRptSideGrp = new();
				((IFixParser)TrdCapRptSideGrp).Parse(viewTrdCapRptSideGrp);
			}
			CopyMsgIndicator = view.GetBool(797);
			PublishTrdIndicator = view.GetBool(852);
			ShortSaleReason = view.GetInt32(853);
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
				case "TradeReportID":
				{
					value = TradeReportID;
					break;
				}
				case "TradeReportTransType":
				{
					value = TradeReportTransType;
					break;
				}
				case "TradeReportType":
				{
					value = TradeReportType;
					break;
				}
				case "TradeRequestID":
				{
					value = TradeRequestID;
					break;
				}
				case "TrdType":
				{
					value = TrdType;
					break;
				}
				case "TrdSubType":
				{
					value = TrdSubType;
					break;
				}
				case "SecondaryTrdType":
				{
					value = SecondaryTrdType;
					break;
				}
				case "TransferReason":
				{
					value = TransferReason;
					break;
				}
				case "ExecType":
				{
					value = ExecType;
					break;
				}
				case "TotNumTradeReports":
				{
					value = TotNumTradeReports;
					break;
				}
				case "LastRptRequested":
				{
					value = LastRptRequested;
					break;
				}
				case "UnsolicitedIndicator":
				{
					value = UnsolicitedIndicator;
					break;
				}
				case "SubscriptionRequestType":
				{
					value = SubscriptionRequestType;
					break;
				}
				case "TradeReportRefID":
				{
					value = TradeReportRefID;
					break;
				}
				case "SecondaryTradeReportRefID":
				{
					value = SecondaryTradeReportRefID;
					break;
				}
				case "SecondaryTradeReportID":
				{
					value = SecondaryTradeReportID;
					break;
				}
				case "TradeLinkID":
				{
					value = TradeLinkID;
					break;
				}
				case "TrdMatchID":
				{
					value = TrdMatchID;
					break;
				}
				case "ExecID":
				{
					value = ExecID;
					break;
				}
				case "OrdStatus":
				{
					value = OrdStatus;
					break;
				}
				case "SecondaryExecID":
				{
					value = SecondaryExecID;
					break;
				}
				case "ExecRestatementReason":
				{
					value = ExecRestatementReason;
					break;
				}
				case "PreviouslyReported":
				{
					value = PreviouslyReported;
					break;
				}
				case "PriceType":
				{
					value = PriceType;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "FinancingDetails":
				{
					value = FinancingDetails;
					break;
				}
				case "OrderQtyData":
				{
					value = OrderQtyData;
					break;
				}
				case "QtyType":
				{
					value = QtyType;
					break;
				}
				case "YieldData":
				{
					value = YieldData;
					break;
				}
				case "UndInstrmtGrp":
				{
					value = UndInstrmtGrp;
					break;
				}
				case "UnderlyingTradingSessionID":
				{
					value = UnderlyingTradingSessionID;
					break;
				}
				case "UnderlyingTradingSessionSubID":
				{
					value = UnderlyingTradingSessionSubID;
					break;
				}
				case "LastQty":
				{
					value = LastQty;
					break;
				}
				case "LastPx":
				{
					value = LastPx;
					break;
				}
				case "LastParPx":
				{
					value = LastParPx;
					break;
				}
				case "LastSpotRate":
				{
					value = LastSpotRate;
					break;
				}
				case "LastForwardPoints":
				{
					value = LastForwardPoints;
					break;
				}
				case "LastMkt":
				{
					value = LastMkt;
					break;
				}
				case "TradeDate":
				{
					value = TradeDate;
					break;
				}
				case "ClearingBusinessDate":
				{
					value = ClearingBusinessDate;
					break;
				}
				case "AvgPx":
				{
					value = AvgPx;
					break;
				}
				case "SpreadOrBenchmarkCurveData":
				{
					value = SpreadOrBenchmarkCurveData;
					break;
				}
				case "AvgPxIndicator":
				{
					value = AvgPxIndicator;
					break;
				}
				case "PositionAmountData":
				{
					value = PositionAmountData;
					break;
				}
				case "MultiLegReportingType":
				{
					value = MultiLegReportingType;
					break;
				}
				case "TradeLegRefID":
				{
					value = TradeLegRefID;
					break;
				}
				case "TrdInstrmtLegGrp":
				{
					value = TrdInstrmtLegGrp;
					break;
				}
				case "TransactTime":
				{
					value = TransactTime;
					break;
				}
				case "TrdRegTimestamps":
				{
					value = TrdRegTimestamps;
					break;
				}
				case "SettlType":
				{
					value = SettlType;
					break;
				}
				case "SettlDate":
				{
					value = SettlDate;
					break;
				}
				case "MatchStatus":
				{
					value = MatchStatus;
					break;
				}
				case "MatchType":
				{
					value = MatchType;
					break;
				}
				case "TrdCapRptSideGrp":
				{
					value = TrdCapRptSideGrp;
					break;
				}
				case "CopyMsgIndicator":
				{
					value = CopyMsgIndicator;
					break;
				}
				case "PublishTrdIndicator":
				{
					value = PublishTrdIndicator;
					break;
				}
				case "ShortSaleReason":
				{
					value = ShortSaleReason;
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
			TradeReportID = null;
			TradeReportTransType = null;
			TradeReportType = null;
			TradeRequestID = null;
			TrdType = null;
			TrdSubType = null;
			SecondaryTrdType = null;
			TransferReason = null;
			ExecType = null;
			TotNumTradeReports = null;
			LastRptRequested = null;
			UnsolicitedIndicator = null;
			SubscriptionRequestType = null;
			TradeReportRefID = null;
			SecondaryTradeReportRefID = null;
			SecondaryTradeReportID = null;
			TradeLinkID = null;
			TrdMatchID = null;
			ExecID = null;
			OrdStatus = null;
			SecondaryExecID = null;
			ExecRestatementReason = null;
			PreviouslyReported = null;
			PriceType = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)OrderQtyData)?.Reset();
			QtyType = null;
			((IFixReset?)YieldData)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			UnderlyingTradingSessionID = null;
			UnderlyingTradingSessionSubID = null;
			LastQty = null;
			LastPx = null;
			LastParPx = null;
			LastSpotRate = null;
			LastForwardPoints = null;
			LastMkt = null;
			TradeDate = null;
			ClearingBusinessDate = null;
			AvgPx = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			AvgPxIndicator = null;
			((IFixReset?)PositionAmountData)?.Reset();
			MultiLegReportingType = null;
			TradeLegRefID = null;
			((IFixReset?)TrdInstrmtLegGrp)?.Reset();
			TransactTime = null;
			((IFixReset?)TrdRegTimestamps)?.Reset();
			SettlType = null;
			SettlDate = null;
			MatchStatus = null;
			MatchType = null;
			((IFixReset?)TrdCapRptSideGrp)?.Reset();
			CopyMsgIndicator = null;
			PublishTrdIndicator = null;
			ShortSaleReason = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
