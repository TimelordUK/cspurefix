using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("r", FixVersion.FIX44)]
	public sealed class OrderMassCancelReport : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 3)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 4)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 530, Type = TagType.String, Offset = 5)]
		public string? MassCancelRequestType { get; set; }
		
		[TagDetails(Tag = 531, Type = TagType.String, Offset = 6)]
		public string? MassCancelResponse { get; set; }
		
		[TagDetails(Tag = 532, Type = TagType.String, Offset = 7)]
		public string? MassCancelRejectReason { get; set; }
		
		[TagDetails(Tag = 533, Type = TagType.Int, Offset = 8)]
		public int? TotalAffectedOrders { get; set; }
		
		[Component(Offset = 9)]
		public AffectedOrdGrp? AffectedOrdGrp { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 12)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 13)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 14)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 15)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 16)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 17)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 18)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 19)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
