using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BC", FixVersion.FIX44)]
	public sealed class NetworkCounterpartySystemStatusRequest : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 935, Type = TagType.Int, Offset = 1, Required = true)]
		public int? NetworkRequestType { get; set; }
		
		[TagDetails(Tag = 933, Type = TagType.String, Offset = 2, Required = true)]
		public string? NetworkRequestID { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public CompIDReqGrp? CompIDReqGrp { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
