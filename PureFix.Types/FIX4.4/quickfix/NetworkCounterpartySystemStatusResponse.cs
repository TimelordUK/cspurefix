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
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(937)]
		public int? NetworkStatusResponseType { get; set; } // INT
		
		[TagDetails(933)]
		public string? NetworkRequestID { get; set; } // STRING
		
		[TagDetails(932)]
		public string? NetworkResponseID { get; set; } // STRING
		
		[TagDetails(934)]
		public string? LastNetworkResponseID { get; set; } // STRING
		
		public CompIDStatGrp? CompIDStatGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
