using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AE", FixVersion.FIX44)]
	public sealed class TradeCaptureReport : FixMsg
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
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 40, Required = false)]
		public DateTime? ClearingBusinessDate { get; set; }
		
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
		public DateTime? SettlDate { get; set; }
		
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
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
