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
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 1, Required = true)]
		public string? RFQReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public RFQReqGrp? RFQReqGrp { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
