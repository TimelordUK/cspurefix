using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AG", FixVersion.FIX44)]
	public sealed class QuoteRequestReject : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = true)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 2, Required = false)]
		public string? RFQReqID { get; set; }
		
		[TagDetails(Tag = 658, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteRequestRejectReason { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public QuotReqRjctGrp? QuotReqRjctGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
