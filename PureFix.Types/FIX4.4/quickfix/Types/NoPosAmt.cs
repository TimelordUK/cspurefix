using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPosAmt
	{
		[TagDetails(707)]
		public string? PosAmtType { get; set; } // STRING
		
		[TagDetails(708)]
		public double? PosAmt { get; set; } // AMT
		
	}
}
