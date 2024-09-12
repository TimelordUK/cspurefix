using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoUnderlyingSecurityAltID
	{
		[TagDetails(458)]
		public string? UnderlyingSecurityAltID { get; set; } // STRING
		
		[TagDetails(459)]
		public string? UnderlyingSecurityAltIDSource { get; set; } // STRING
		
	}
}
