using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AP", FixVersion.FIX44)]
	public sealed class PositionReport : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(721, TagType.String)]
		public string? PosMaintRptID { get; set; }
		
		[TagDetails(710, TagType.String)]
		public string? PosReqID { get; set; }
		
		[TagDetails(724, TagType.Int)]
		public int? PosReqType { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(727, TagType.Int)]
		public int? TotalNumPosReports { get; set; }
		
		[TagDetails(325, TagType.Boolean)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(728, TagType.Int)]
		public int? PosReqResult { get; set; }
		
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
		
		[TagDetails(730, TagType.Float)]
		public double? SettlPrice { get; set; }
		
		[TagDetails(731, TagType.Int)]
		public int? SettlPriceType { get; set; }
		
		[TagDetails(734, TagType.Float)]
		public double? PriorSettlPrice { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component]
		public PosUndInstrmtGrp? PosUndInstrmtGrp { get; set; }
		
		[Component]
		public PositionQty? PositionQty { get; set; }
		
		[Component]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(506, TagType.String)]
		public string? RegistStatus { get; set; }
		
		[TagDetails(743, TagType.LocalDate)]
		public DateTime? DeliveryDate { get; set; }
		
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
