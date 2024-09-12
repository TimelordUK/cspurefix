using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class DerivativeSecurityListRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(320, TagType.String)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(559, TagType.Int)]
		public int? SecurityListRequestType { get; set; }
		
		[Component]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(762, TagType.String)]
		public string? SecuritySubType { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
