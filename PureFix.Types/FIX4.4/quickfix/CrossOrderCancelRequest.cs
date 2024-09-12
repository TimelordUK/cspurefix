using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class CrossOrderCancelRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(548)]
		public string? CrossID { get; set; } // STRING
		
		[TagDetails(551)]
		public string? OrigCrossID { get; set; } // STRING
		
		[TagDetails(549)]
		public int? CrossType { get; set; } // INT
		
		[TagDetails(550)]
		public int? CrossPrioritization { get; set; } // INT
		
		public SideCrossOrdCxlGrp? SideCrossOrdCxlGrp { get; set; }
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
