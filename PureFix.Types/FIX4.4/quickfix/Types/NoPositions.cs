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
		[TagDetails(703)]
		public string? PosType { get; set; } // STRING
		
		[TagDetails(704)]
		public double? LongQty { get; set; } // QTY
		
		[TagDetails(705)]
		public double? ShortQty { get; set; } // QTY
		
		[TagDetails(706)]
		public int? PosQtyStatus { get; set; } // INT
		
		public NestedParties? NestedParties { get; set; }
	}
}
