using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoLegSecurityAltID
	{
		public string? LegSecurityAltID { get; set; } // 605 STRING
		public string? LegSecurityAltIDSource { get; set; } // 606 STRING
	}
}
