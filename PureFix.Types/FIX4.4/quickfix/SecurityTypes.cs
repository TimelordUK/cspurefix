using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("w", FixVersion.FIX44)]
	public sealed class SecurityTypes : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(Tag = 323, Type = TagType.Int, Offset = 3)]
		public int? SecurityResponseType { get; set; }
		
		[TagDetails(Tag = 557, Type = TagType.Int, Offset = 4)]
		public int? TotNoSecurityTypes { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 5)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 6)]
		public SecTypesGrp? SecTypesGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 12)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 13)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
