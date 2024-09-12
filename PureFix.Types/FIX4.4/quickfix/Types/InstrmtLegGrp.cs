using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class InstrmtLegGrp
	{
		[Group(NoOfTag = 555, Offset = 0)]
		public NoLegs[]? NoLegs { get; set; }
		
	}
}
