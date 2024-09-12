using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class RFQRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(644)]
		public string? RFQReqID { get; set; } // STRING
		
		public RFQReqGrp? RFQReqGrp { get; set; }
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
