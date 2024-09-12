using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoSecurityAltID
	{
		public string? SecurityAltID { get; set; } // 455 STRING
		public string? SecurityAltIDSource { get; set; } // 456 STRING
	}
}
