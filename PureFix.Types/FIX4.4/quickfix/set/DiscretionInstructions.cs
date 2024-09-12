using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class DiscretionInstructions
	{
		public string? DiscretionInst { get; set; } // 388 CHAR
		public double? DiscretionOffsetValue { get; set; } // 389 FLOAT
		public int? DiscretionMoveType { get; set; } // 841 INT
		public int? DiscretionOffsetType { get; set; } // 842 INT
		public int? DiscretionLimitType { get; set; } // 843 INT
		public int? DiscretionRoundDirection { get; set; } // 844 INT
		public int? DiscretionScope { get; set; } // 846 INT
	}
}
