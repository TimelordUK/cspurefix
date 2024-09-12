using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MarketDataIncrementalRefresh : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(262)]
		public string? MDReqID { get; set; } // STRING
		
		public MDIncGrp? MDIncGrp { get; set; }
		[TagDetails(813)]
		public int? ApplQueueDepth { get; set; } // INT
		
		[TagDetails(814)]
		public int? ApplQueueResolution { get; set; } // INT
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
