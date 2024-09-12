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
		[TagDetails(605)]
		public string? LegSecurityAltID { get; set; } // STRING
		
		[TagDetails(606)]
		public string? LegSecurityAltIDSource { get; set; } // STRING
		
	}
}
