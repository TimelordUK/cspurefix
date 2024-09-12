using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoExecs
	{
		[TagDetails(32)]
		public double? LastQty { get; set; } // QTY
		
		[TagDetails(17)]
		public string? ExecID { get; set; } // STRING
		
		[TagDetails(527)]
		public string? SecondaryExecID { get; set; } // STRING
		
		[TagDetails(31)]
		public double? LastPx { get; set; } // PRICE
		
		[TagDetails(669)]
		public double? LastParPx { get; set; } // PRICE
		
		[TagDetails(29)]
		public string? LastCapacity { get; set; } // CHAR
		
	}
}
