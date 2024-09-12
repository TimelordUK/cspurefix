using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class ListStrikePrice : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ListID { get; set; } // 66 STRING
		public int? TotNoStrikes { get; set; } // 422 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public InstrmtStrkPxGrp? InstrmtStrkPxGrp { get; set; }
		public UndInstrmtStrkPxGrp? UndInstrmtStrkPxGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
