using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoStipulations
	{
		public string? StipulationType { get; set; } // 233 STRING
		public string? StipulationValue { get; set; } // 234 STRING
	}
}
