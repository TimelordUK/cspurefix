using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class BusinessMessageReject : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(45, TagType.Int)]
		public int? RefSeqNum { get; set; }
		
		[TagDetails(372, TagType.String)]
		public string? RefMsgType { get; set; }
		
		[TagDetails(379, TagType.String)]
		public string? BusinessRejectRefID { get; set; }
		
		[TagDetails(380, TagType.Int)]
		public int? BusinessRejectReason { get; set; }
		
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
