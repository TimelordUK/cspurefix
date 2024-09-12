using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class UndSecAltIDGrp
	{
		[Group(NoOfTag = 457, Offset = 0, Required = false)]
		public NoUnderlyingSecurityAltID[]? NoUnderlyingSecurityAltID { get; set; }
		
	}
}
