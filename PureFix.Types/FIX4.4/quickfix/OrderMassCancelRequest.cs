using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("q", FixVersion.FIX44)]
	public sealed class OrderMassCancelRequest : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 530, Type = TagType.String, Offset = 3, Required = true)]
		public string? MassCancelRequestType { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 8, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 9, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 10, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 11, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 12, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 13, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
