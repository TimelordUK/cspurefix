using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UndSecAltIDGrpNoUnderlyingSecurityAltID : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 458, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingSecurityAltID { get; set; }
		
		[TagDetails(Tag = 459, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingSecurityAltIDSource { get; set; }
		
	}
}
