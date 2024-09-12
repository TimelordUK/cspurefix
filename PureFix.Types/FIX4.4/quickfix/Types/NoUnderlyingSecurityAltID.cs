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
		[TagDetails(458, TagType.String)]
		public string? UnderlyingSecurityAltID { get; set; }
		
		[TagDetails(459, TagType.String)]
		public string? UnderlyingSecurityAltIDSource { get; set; }
		
	}
}
