using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("Y", FixVersion.FIX44)]
	public sealed class MarketDataRequestReject : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1)]
		public string? MDReqID { get; set; }
		
		[TagDetails(Tag = 281, Type = TagType.String, Offset = 2)]
		public string? MDReqRejReason { get; set; }
		
		[Component(Offset = 3)]
		public MDRjctGrp? MDRjctGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 4)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 5)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 6)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 7)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
