using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("j", FixVersion.FIX44)]
	public sealed class BusinessMessageReject : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 45, Type = TagType.Int, Offset = 1)]
		public int? RefSeqNum { get; set; }
		
		[TagDetails(Tag = 372, Type = TagType.String, Offset = 2)]
		public string? RefMsgType { get; set; }
		
		[TagDetails(Tag = 379, Type = TagType.String, Offset = 3)]
		public string? BusinessRejectRefID { get; set; }
		
		[TagDetails(Tag = 380, Type = TagType.Int, Offset = 4)]
		public int? BusinessRejectReason { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
