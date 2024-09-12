using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoInstrAttrib
	{
		public int? InstrAttribType { get; set; } // 871 INT
		public string? InstrAttribValue { get; set; } // 872 STRING
	}
}
