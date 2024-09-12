using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MarketDataSnapshotFullRefresh : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(262)]
		public string? MDReqID { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(291)]
		public string? FinancialStatus { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(292)]
		public string? CorporateAction { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(451)]
		public double? NetChgPrevDay { get; set; } // PRICEOFFSET
		
		public MDFullGrp? MDFullGrp { get; set; }
		[TagDetails(813)]
		public int? ApplQueueDepth { get; set; } // INT
		
		[TagDetails(814)]
		public int? ApplQueueResolution { get; set; } // INT
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
