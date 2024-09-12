using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoPosAmt
	{
		public string? PosAmtType { get; set; } // 707 STRING
		public double? PosAmt { get; set; } // 708 AMT
	}
}
