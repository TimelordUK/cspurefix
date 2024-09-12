using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoStipulations
	{
		public string? StipulationType { get; set; } // 233 STRING
		public string? StipulationValue { get; set; } // 234 STRING
	}
}
