using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class QuoteRequestReject : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(131, TagType.String)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(644, TagType.String)]
		public string? RFQReqID { get; set; }
		
		[TagDetails(658, TagType.Int)]
		public int? QuoteRequestRejectReason { get; set; }
		
		[Component]
		public QuotReqRjctGrp? QuotReqRjctGrp { get; set; }
		
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
