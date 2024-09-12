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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(935, TagType.Int)]
		public int? NetworkRequestType { get; set; }
		
		[TagDetails(933, TagType.String)]
		public string? NetworkRequestID { get; set; }
		
		[Component]
		public CompIDReqGrp? CompIDReqGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
