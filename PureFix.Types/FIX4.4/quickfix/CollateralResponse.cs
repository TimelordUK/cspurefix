using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class CollateralResponse : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(904, TagType.String)]
		public string? CollRespID { get; set; }
		
		[TagDetails(902, TagType.String)]
		public string? CollAsgnID { get; set; }
		
		[TagDetails(894, TagType.String)]
		public string? CollReqID { get; set; }
		
		[TagDetails(895, TagType.Int)]
		public int? CollAsgnReason { get; set; }
		
		[TagDetails(903, TagType.Int)]
		public int? CollAsgnTransType { get; set; }
		
		[TagDetails(905, TagType.Int)]
		public int? CollAsgnRespType { get; set; }
		
		[TagDetails(906, TagType.Int)]
		public int? CollAsgnRejectReason { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
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
		public UndInstrmtCollGrp? UndInstrmtCollGrp { get; set; }
		
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
		
		[Component]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
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
