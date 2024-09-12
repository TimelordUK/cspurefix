using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MarketDataRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? MDReqID { get; set; } // 262 STRING
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public int? MarketDepth { get; set; } // 264 INT
		public int? MDUpdateType { get; set; } // 265 INT
		public bool? AggregatedBook { get; set; } // 266 BOOLEAN
		public string? OpenCloseSettlFlag { get; set; } // 286 MULTIPLEVALUESTRING
		public string? Scope { get; set; } // 546 MULTIPLEVALUESTRING
		public bool? MDImplicitDelete { get; set; } // 547 BOOLEAN
		public MDReqGrp? MDReqGrp { get; set; }
		public InstrmtMDReqGrp? InstrmtMDReqGrp { get; set; }
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		public int? ApplQueueAction { get; set; } // 815 INT
		public int? ApplQueueMax { get; set; } // 812 INT
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
