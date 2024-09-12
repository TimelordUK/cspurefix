using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoInstrAttrib
	{
		public int? InstrAttribType { get; set; } // 871 INT
		public string? InstrAttribValue { get; set; } // 872 STRING
	}
}
