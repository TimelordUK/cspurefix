using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegSecAltIDGrpNoLegSecurityAltID : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 605, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegSecurityAltID { get; set; }
		
		[TagDetails(Tag = 606, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegSecurityAltIDSource { get; set; }
		
	}
}
