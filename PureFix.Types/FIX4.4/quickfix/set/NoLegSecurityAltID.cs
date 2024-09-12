using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoLegSecurityAltID
	{
		public string? LegSecurityAltID { get; set; } // 605 STRING
		public string? LegSecurityAltIDSource { get; set; } // 606 STRING
	}
}
