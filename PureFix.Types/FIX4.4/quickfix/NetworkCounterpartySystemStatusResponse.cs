using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class NetworkCounterpartySystemStatusResponse : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(937, TagType.Int)]
		public int? NetworkStatusResponseType { get; set; }
		
		[TagDetails(933, TagType.String)]
		public string? NetworkRequestID { get; set; }
		
		[TagDetails(932, TagType.String)]
		public string? NetworkResponseID { get; set; }
		
		[TagDetails(934, TagType.String)]
		public string? LastNetworkResponseID { get; set; }
		
		[Component]
		public CompIDStatGrp? CompIDStatGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
