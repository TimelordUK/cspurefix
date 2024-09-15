using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecTypesGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 558, Offset = 0, Required = false)]
		public SecTypesGrpNoSecurityTypes[]? NoSecurityTypes { get; set; }
		
	}
}
