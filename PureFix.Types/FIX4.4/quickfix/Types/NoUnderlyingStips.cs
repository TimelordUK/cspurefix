using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoUnderlyingStips
	{
		[TagDetails(888)]
		public string? UnderlyingStipType { get; set; } // STRING
		
		[TagDetails(889)]
		public string? UnderlyingStipValue { get; set; } // STRING
		
	}
}
