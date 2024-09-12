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
		[TagDetails(455, TagType.String)]
		public string? SecurityAltID { get; set; }
		
		[TagDetails(456, TagType.String)]
		public string? SecurityAltIDSource { get; set; }
		
	}
}
