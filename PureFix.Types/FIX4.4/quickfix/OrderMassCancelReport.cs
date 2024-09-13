using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("r", FixVersion.FIX44)]
	public sealed class OrderMassCancelReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 3, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 530, Type = TagType.String, Offset = 5, Required = true)]
		public string? MassCancelRequestType { get; set; }
		
		[TagDetails(Tag = 531, Type = TagType.String, Offset = 6, Required = true)]
		public string? MassCancelResponse { get; set; }
		
		[TagDetails(Tag = 532, Type = TagType.String, Offset = 7, Required = false)]
		public string? MassCancelRejectReason { get; set; }
		
		[TagDetails(Tag = 533, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TotalAffectedOrders { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public AffectedOrdGrp? AffectedOrdGrp { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 14, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 15, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 16, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 17, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 18, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 19, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
