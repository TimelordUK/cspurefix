using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("e", FixVersion.FIX44)]
	public sealed class SecurityStatusRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1)]
		public string? SecurityStatusReqID { get; set; }
		
		[Component(Offset = 2)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 3)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 4)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 5)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 7)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 10)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
