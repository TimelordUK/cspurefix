using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BG", FixVersion.FIX44)]
	public sealed class CollateralInquiryAck : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 909, Type = TagType.String, Offset = 1)]
		public string? CollInquiryID { get; set; }
		
		[TagDetails(Tag = 945, Type = TagType.Int, Offset = 2)]
		public int? CollInquiryStatus { get; set; }
		
		[TagDetails(Tag = 946, Type = TagType.Int, Offset = 3)]
		public int? CollInquiryResult { get; set; }
		
		[Component(Offset = 4)]
		public CollInqQualGrp? CollInqQualGrp { get; set; }
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 5)]
		public int? TotNumReports { get; set; }
		
		[Component(Offset = 6)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 8)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 9)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 10)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 11)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 12)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component(Offset = 13)]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component(Offset = 14)]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component(Offset = 15)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 16)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 17)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 18)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 19)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 20)]
		public string? Currency { get; set; }
		
		[Component(Offset = 21)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 22)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 23)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 24)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 25)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 26)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 27)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 28)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 29)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 30)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 31)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 32)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 33)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
