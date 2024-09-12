using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("g", FixVersion.FIX44)]
	public sealed class TradingSessionStatusRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 335, Type = TagType.String, Offset = 1)]
		public string? TradSesReqID { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 3)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 338, Type = TagType.Int, Offset = 4)]
		public int? TradSesMethod { get; set; }
		
		[TagDetails(Tag = 339, Type = TagType.Int, Offset = 5)]
		public int? TradSesMode { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 6)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 7)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
