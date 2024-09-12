using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BB")]
	public sealed class CollateralInquiry : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(909, TagType.String)]
		public string? CollInquiryID { get; set; }
		
		[Component]
		public CollInqQualGrp? CollInqQualGrp { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(725, TagType.Int)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(726, TagType.String)]
		public string? ResponseDestination { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(198, TagType.String)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(526, TagType.String)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(53, TagType.Float)]
		public double? Quantity { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(899, TagType.Float)]
		public double? MarginExcess { get; set; }
		
		[TagDetails(900, TagType.Float)]
		public double? TotalNetValue { get; set; }
		
		[TagDetails(901, TagType.Float)]
		public double? CashOutstanding { get; set; }
		
		[Component]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(44, TagType.Float)]
		public double? Price { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(159, TagType.Float)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(920, TagType.Float)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(921, TagType.Float)]
		public double? StartCash { get; set; }
		
		[TagDetails(922, TagType.Float)]
		public double? EndCash { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[Component]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(716, TagType.String)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(717, TagType.String)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(715, TagType.LocalDate)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
