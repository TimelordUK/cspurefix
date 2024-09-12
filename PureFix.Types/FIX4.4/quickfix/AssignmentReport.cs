using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AW", FixVersion.FIX44)]
	public sealed class AssignmentReport : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 833, Type = TagType.String, Offset = 1, Required = true)]
		public string? AsgnRptID { get; set; }
		
		[TagDetails(Tag = 832, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TotNumAssignmentReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? LastRptRequested { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 6, Required = true)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 10, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 11, Required = true)]
		public PositionQty? PositionQty { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(Tag = 834, Type = TagType.Float, Offset = 13, Required = false)]
		public double? ThresholdAmount { get; set; }
		
		[TagDetails(Tag = 730, Type = TagType.Float, Offset = 14, Required = true)]
		public double? SettlPrice { get; set; }
		
		[TagDetails(Tag = 731, Type = TagType.Int, Offset = 15, Required = true)]
		public int? SettlPriceType { get; set; }
		
		[TagDetails(Tag = 732, Type = TagType.Float, Offset = 16, Required = true)]
		public double? UnderlyingSettlPrice { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(Tag = 744, Type = TagType.String, Offset = 18, Required = true)]
		public string? AssignmentMethod { get; set; }
		
		[TagDetails(Tag = 745, Type = TagType.Float, Offset = 19, Required = false)]
		public double? AssignmentUnit { get; set; }
		
		[TagDetails(Tag = 746, Type = TagType.Float, Offset = 20, Required = true)]
		public double? OpenInterest { get; set; }
		
		[TagDetails(Tag = 747, Type = TagType.String, Offset = 21, Required = true)]
		public string? ExerciseMethod { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 22, Required = true)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 23, Required = true)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 24, Required = true)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 25, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 26, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 27, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 28, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
