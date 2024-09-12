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
		public int? NetworkStatusResponseType { get; set; } // 937 INT
		public string? NetworkRequestID { get; set; } // 933 STRING
		public string? NetworkResponseID { get; set; } // 932 STRING
		public string? LastNetworkResponseID { get; set; } // 934 STRING
		public CompIDStatGrp? CompIDStatGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
