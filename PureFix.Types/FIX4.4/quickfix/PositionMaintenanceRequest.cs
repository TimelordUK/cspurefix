using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AL", FixVersion.FIX44)]
	public sealed class PositionMaintenanceRequest : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosReqID { get; set; }
		
		[TagDetails(Tag = 709, Type = TagType.Int, Offset = 2, Required = true)]
		public int? PosTransType { get; set; }
		
		[TagDetails(Tag = 712, Type = TagType.Int, Offset = 3, Required = true)]
		public int? PosMaintAction { get; set; }
		
		[TagDetails(Tag = 713, Type = TagType.String, Offset = 4, Required = false)]
		public string? OrigPosReqRefID { get; set; }
		
		[TagDetails(Tag = 714, Type = TagType.String, Offset = 5, Required = false)]
		public string? PosMaintRptRefID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 6, Required = true)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 7, Required = false)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 8, Required = false)]
		public string? SettlSessSubID { get; set; }
		
		[Component(Offset = 9, Required = true)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 10, Required = true)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 11, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 12, Required = true)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 13, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 14, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 15, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 16, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 19, Required = true)]
		public PositionQty? PositionQty { get; set; }
		
		[TagDetails(Tag = 718, Type = TagType.Int, Offset = 20, Required = false)]
		public int? AdjustmentType { get; set; }
		
		[TagDetails(Tag = 719, Type = TagType.Boolean, Offset = 21, Required = false)]
		public bool? ContraryInstructionIndicator { get; set; }
		
		[TagDetails(Tag = 720, Type = TagType.Boolean, Offset = 22, Required = false)]
		public bool? PriorSpreadIndicator { get; set; }
		
		[TagDetails(Tag = 834, Type = TagType.Float, Offset = 23, Required = false)]
		public double? ThresholdAmount { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 27, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
