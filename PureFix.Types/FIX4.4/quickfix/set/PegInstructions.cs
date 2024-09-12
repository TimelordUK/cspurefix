using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class PegInstructions
	{
		public double? PegOffsetValue { get; set; } // 211 FLOAT
		public int? PegMoveType { get; set; } // 835 INT
		public int? PegOffsetType { get; set; } // 836 INT
		public int? PegLimitType { get; set; } // 837 INT
		public int? PegRoundDirection { get; set; } // 838 INT
		public int? PegScope { get; set; } // 840 INT
	}
}
