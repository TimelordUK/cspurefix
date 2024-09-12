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
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID { get; set; }
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 2, Required = false)]
		public string? PosReqID { get; set; }
		
		[TagDetails(Tag = 724, Type = TagType.Int, Offset = 3, Required = false)]
		public int? PosReqType { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 4, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 727, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TotalNumPosReports { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 728, Type = TagType.Int, Offset = 7, Required = true)]
		public int? PosReqResult { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 8, Required = true)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 9, Required = false)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 10, Required = false)]
		public string? SettlSessSubID { get; set; }
		
		[Component(Offset = 11, Required = true)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 12, Required = true)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 13, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 14, Required = true)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 15, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 16, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 730, Type = TagType.Float, Offset = 17, Required = true)]
		public double? SettlPrice { get; set; }
		
		[TagDetails(Tag = 731, Type = TagType.Int, Offset = 18, Required = true)]
		public int? SettlPriceType { get; set; }
		
		[TagDetails(Tag = 734, Type = TagType.Float, Offset = 19, Required = true)]
		public double? PriorSettlPrice { get; set; }
		
		[Component(Offset = 20, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public PosUndInstrmtGrp? PosUndInstrmtGrp { get; set; }
		
		[Component(Offset = 22, Required = true)]
		public PositionQty? PositionQty { get; set; }
		
		[Component(Offset = 23, Required = true)]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(Tag = 506, Type = TagType.String, Offset = 24, Required = false)]
		public string? RegistStatus { get; set; }
		
		[TagDetails(Tag = 743, Type = TagType.LocalDate, Offset = 25, Required = false)]
		public DateTime? DeliveryDate { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 26, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 27, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 28, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 29, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
