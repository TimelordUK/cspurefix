using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("R", FixVersion.FIX44)]
	public sealed class QuoteRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 2)]
		public string? RFQReqID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 4)]
		public string? OrderCapacity { get; set; }
		
		[Component(Offset = 5)]
		public QuotReqGrp? QuotReqGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 6)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 7)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 8)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 9)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
