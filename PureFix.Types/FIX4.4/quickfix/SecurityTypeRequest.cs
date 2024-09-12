using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("v", FixVersion.FIX44)]
	public sealed class SecurityTypeRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 2)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 3)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 4)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 5)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 6)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 7)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 8)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 762, Type = TagType.String, Offset = 9)]
		public string? SecuritySubType { get; set; }
		
		[Component(Offset = 10)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
