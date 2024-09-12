using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPositions
	{
		public string? PosType { get; set; } // 703 STRING
		public double? LongQty { get; set; } // 704 QTY
		public double? ShortQty { get; set; } // 705 QTY
		public int? PosQtyStatus { get; set; } // 706 INT
		public NestedParties? NestedParties { get; set; }
	}
}
