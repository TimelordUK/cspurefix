using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoStipulations
	{
		[TagDetails(233)]
		public string? StipulationType { get; set; } // STRING
		
		[TagDetails(234)]
		public string? StipulationValue { get; set; } // STRING
		
	}
}
