using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class PositionMaintenanceReport : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(721, TagType.String)]
		public string? PosMaintRptID { get; set; }
		
		[TagDetails(709, TagType.Int)]
		public int? PosTransType { get; set; }
		
		[TagDetails(710, TagType.String)]
		public string? PosReqID { get; set; }
		
		[TagDetails(712, TagType.Int)]
		public int? PosMaintAction { get; set; }
		
		[TagDetails(713, TagType.String)]
		public string? OrigPosReqRefID { get; set; }
		
		[TagDetails(722, TagType.Int)]
		public int? PosMaintStatus { get; set; }
		
		[TagDetails(723, TagType.Int)]
		public int? PosMaintResult { get; set; }
		
		[TagDetails(715, TagType.LocalDate)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(716, TagType.String)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(717, TagType.String)]
		public string? SettlSessSubID { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public PositionQty? PositionQty { get; set; }
		
		[Component]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(718, TagType.Int)]
		public int? AdjustmentType { get; set; }
		
		[TagDetails(834, TagType.Float)]
		public double? ThresholdAmount { get; set; }
		
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
