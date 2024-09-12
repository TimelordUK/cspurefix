using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoUnderlyingSecurityAltID
	{
		public string? UnderlyingSecurityAltID { get; set; } // 458 STRING
		public string? UnderlyingSecurityAltIDSource { get; set; } // 459 STRING
	}
}
