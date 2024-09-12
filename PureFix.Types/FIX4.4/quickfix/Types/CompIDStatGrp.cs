using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class CompIDStatGrp
	{
		[Group(NoOfTag = 936, Offset = 0, Required = true)]
		public NoCompIDs[]? NoCompIDs { get; set; }
		
	}
}
