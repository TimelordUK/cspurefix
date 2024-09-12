using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoLegStipulations
	{
		public string? LegStipulationType { get; set; } // 688 STRING
		public string? LegStipulationValue { get; set; } // 689 STRING
	}
}
