using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class RgstDtlsGrp
	{
		[Group(NoOfTag = 473, Offset = 0)]
		public NoRegistDtls[]? NoRegistDtls { get; set; }
		
	}
}
