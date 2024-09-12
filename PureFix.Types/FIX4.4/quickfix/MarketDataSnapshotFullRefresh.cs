using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class MarketDataSnapshotFullRefresh : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? MDReqID { get; set; } // 262 STRING
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? FinancialStatus { get; set; } // 291 MULTIPLEVALUESTRING
		public string? CorporateAction { get; set; } // 292 MULTIPLEVALUESTRING
		public double? NetChgPrevDay { get; set; } // 451 PRICEOFFSET
		public MDFullGrp? MDFullGrp { get; set; }
		public int? ApplQueueDepth { get; set; } // 813 INT
		public int? ApplQueueResolution { get; set; } // 814 INT
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
