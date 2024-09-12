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
		[TagDetails(262)]
		public string? MDReqID { get; set; } // STRING
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(264)]
		public int? MarketDepth { get; set; } // INT
		
		[TagDetails(265)]
		public int? MDUpdateType { get; set; } // INT
		
		[TagDetails(266)]
		public bool? AggregatedBook { get; set; } // BOOLEAN
		
		[TagDetails(286)]
		public string? OpenCloseSettlFlag { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(546)]
		public string? Scope { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(547)]
		public bool? MDImplicitDelete { get; set; } // BOOLEAN
		
		public MDReqGrp? MDReqGrp { get; set; }
		public InstrmtMDReqGrp? InstrmtMDReqGrp { get; set; }
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		[TagDetails(815)]
		public int? ApplQueueAction { get; set; } // INT
		
		[TagDetails(812)]
		public int? ApplQueueMax { get; set; } // INT
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
