using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class RFQRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? RFQReqID { get; set; } // 644 STRING
		public RFQReqGrp? RFQReqGrp { get; set; }
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
