using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AE", FixVersion.FIX44)]
	public sealed partial class TradeCaptureReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 6, Required = false)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 7, Required = false)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 8, Required = false)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 9, Required = false)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 748, Type = TagType.Int, Offset = 10, Required = false)]
		public int? TotNumTradeReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 11, Required = false)]
		public bool? LastRptRequested { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 12, Required = false)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 13, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 14, Required = false)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 16, Required = false)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 17, Required = false)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 18, Required = false)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 19, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 20, Required = false)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 21, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 22, Required = false)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 23, Required = true)]
		public bool? PreviouslyReported { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 24, Required = false)]
		public int? PriceType { get; set; }
		
		[Component(Offset = 25, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 26, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 28, Required = false)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 29, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 30, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 822, Type = TagType.String, Offset = 31, Required = false)]
		public string? UnderlyingTradingSessionID { get; set; }
		
		[TagDetails(Tag = 823, Type = TagType.String, Offset = 32, Required = false)]
		public string? UnderlyingTradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 33, Required = true)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 34, Required = true)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 35, Required = false)]
		public double? LastParPx { get; set; }
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 36, Required = false)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 37, Required = false)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 38, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 39, Required = true)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 40, Required = false)]
		public DateOnly? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 41, Required = false)]
		public double? AvgPx { get; set; }
		
		[Component(Offset = 42, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 819, Type = TagType.Int, Offset = 43, Required = false)]
		public int? AvgPxIndicator { get; set; }
		
		[Component(Offset = 44, Required = false)]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 45, Required = false)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 824, Type = TagType.String, Offset = 46, Required = false)]
		public string? TradeLegRefID { get; set; }
		
		[Component(Offset = 47, Required = false)]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 48, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 49, Required = false)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 50, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 51, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 52, Required = false)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 53, Required = false)]
		public string? MatchType { get; set; }
		
		[Component(Offset = 54, Required = true)]
		public TrdCapRptSideGrp? TrdCapRptSideGrp { get; set; }
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 55, Required = false)]
		public bool? CopyMsgIndicator { get; set; }
		
		[TagDetails(Tag = 852, Type = TagType.Boolean, Offset = 56, Required = false)]
		public bool? PublishTrdIndicator { get; set; }
		
		[TagDetails(Tag = 853, Type = TagType.Int, Offset = 57, Required = false)]
		public int? ShortSaleReason { get; set; }
		
		[Component(Offset = 58, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeReportID is not null
				&& PreviouslyReported is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& LastQty is not null
				&& LastPx is not null
				&& TradeDate is not null
				&& TransactTime is not null
				&& TrdCapRptSideGrp is not null && ((IFixValidator)TrdCapRptSideGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
