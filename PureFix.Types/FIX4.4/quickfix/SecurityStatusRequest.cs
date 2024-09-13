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
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityStatusReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 7, Required = true)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
