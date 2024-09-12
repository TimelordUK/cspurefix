using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoLegStipulations
	{
		[TagDetails(688)]
		public string? LegStipulationType { get; set; } // STRING
		
		[TagDetails(689)]
		public string? LegStipulationValue { get; set; } // STRING
		
	}
}
