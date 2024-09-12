using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityStatusRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(324, TagType.String)]
		public string? SecurityStatusReqID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
