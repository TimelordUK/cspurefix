using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class CrossOrderCancelRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? CrossID { get; set; } // 548 STRING
		public string? OrigCrossID { get; set; } // 551 STRING
		public int? CrossType { get; set; } // 549 INT
		public int? CrossPrioritization { get; set; } // 550 INT
		public SideCrossOrdCxlGrp? SideCrossOrdCxlGrp { get; set; }
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
