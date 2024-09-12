using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AM", FixVersion.FIX44)]
	public sealed class PositionMaintenanceReport : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1)]
		public string? PosMaintRptID { get; set; }
		
		[TagDetails(Tag = 709, Type = TagType.Int, Offset = 2)]
		public int? PosTransType { get; set; }
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 3)]
		public string? PosReqID { get; set; }
		
		[TagDetails(Tag = 712, Type = TagType.Int, Offset = 4)]
		public int? PosMaintAction { get; set; }
		
		[TagDetails(Tag = 713, Type = TagType.String, Offset = 5)]
		public string? OrigPosReqRefID { get; set; }
		
		[TagDetails(Tag = 722, Type = TagType.Int, Offset = 6)]
		public int? PosMaintStatus { get; set; }
		
		[TagDetails(Tag = 723, Type = TagType.Int, Offset = 7)]
		public int? PosMaintResult { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 8)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 9)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 10)]
		public string? SettlSessSubID { get; set; }
		
		[Component(Offset = 11)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 12)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 13)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 14)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 15)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 16)]
		public string? Currency { get; set; }
		
		[Component(Offset = 17)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 18)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 19)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 20)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 21)]
		public PositionQty? PositionQty { get; set; }
		
		[Component(Offset = 22)]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(Tag = 718, Type = TagType.Int, Offset = 23)]
		public int? AdjustmentType { get; set; }
		
		[TagDetails(Tag = 834, Type = TagType.Float, Offset = 24)]
		public double? ThresholdAmount { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 25)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 26)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 27)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 28)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
