using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("3", FixVersion.FIX44)]
	public sealed class Reject : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 45, Type = TagType.Int, Offset = 1, Required = true)]
		public int? RefSeqNum { get; set; }
		
		[TagDetails(Tag = 371, Type = TagType.Int, Offset = 2, Required = false)]
		public int? RefTagID { get; set; }
		
		[TagDetails(Tag = 372, Type = TagType.String, Offset = 3, Required = false)]
		public string? RefMsgType { get; set; }
		
		[TagDetails(Tag = 373, Type = TagType.Int, Offset = 4, Required = false)]
		public int? SessionRejectReason { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
