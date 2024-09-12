using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSecurityAltID
	{
		[TagDetails(455)]
		public string? SecurityAltID { get; set; } // STRING
		
		[TagDetails(456)]
		public string? SecurityAltIDSource { get; set; } // STRING
		
	}
}
