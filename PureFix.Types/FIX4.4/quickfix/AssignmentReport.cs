using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AssignmentReport : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(833, TagType.String)]
		public string? AsgnRptID { get; set; }
		
		[TagDetails(832, TagType.Int)]
		public int? TotNumAssignmentReports { get; set; }
		
		[TagDetails(912, TagType.Boolean)]
		public bool? LastRptRequested { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
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
		public PositionQty? PositionQty { get; set; }
		
		[Component]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(834, TagType.Float)]
		public double? ThresholdAmount { get; set; }
		
		[TagDetails(730, TagType.Float)]
		public double? SettlPrice { get; set; }
		
		[TagDetails(731, TagType.Int)]
		public int? SettlPriceType { get; set; }
		
		[TagDetails(732, TagType.Float)]
		public double? UnderlyingSettlPrice { get; set; }
		
		[TagDetails(432, TagType.LocalDate)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(744, TagType.String)]
		public string? AssignmentMethod { get; set; }
		
		[TagDetails(745, TagType.Float)]
		public double? AssignmentUnit { get; set; }
		
		[TagDetails(746, TagType.Float)]
		public double? OpenInterest { get; set; }
		
		[TagDetails(747, TagType.String)]
		public string? ExerciseMethod { get; set; }
		
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
