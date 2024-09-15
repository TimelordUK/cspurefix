using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecAltIDGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 454, Offset = 0, Required = false)]
		public SecAltIDGrpNoSecurityAltID[]? NoSecurityAltID { get; set; }
		
	}
}
