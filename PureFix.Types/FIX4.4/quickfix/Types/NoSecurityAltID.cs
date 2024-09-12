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
		[TagDetails(Tag = 455, Type = TagType.String, Offset = 0)]
		public string? SecurityAltID { get; set; }
		
		[TagDetails(Tag = 456, Type = TagType.String, Offset = 1)]
		public string? SecurityAltIDSource { get; set; }
		
	}
}
