using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class NetworkCounterpartySystemStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(935)]
		public int? NetworkRequestType { get; set; } // INT
		
		[TagDetails(933)]
		public string? NetworkRequestID { get; set; } // STRING
		
		public CompIDReqGrp? CompIDReqGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
