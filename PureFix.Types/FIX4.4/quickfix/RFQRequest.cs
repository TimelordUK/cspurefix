using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AH", FixVersion.FIX44)]
	public sealed class RFQRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 1)]
		public string? RFQReqID { get; set; }
		
		[Component(Offset = 2)]
		public RFQReqGrp? RFQReqGrp { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 4)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
