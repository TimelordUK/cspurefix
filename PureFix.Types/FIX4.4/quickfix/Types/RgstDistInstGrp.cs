using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class RgstDistInstGrp
	{
		[Group(NoOfTag = 510, Offset = 0, Required = false)]
		public NoDistribInsts[]? NoDistribInsts { get; set; }
		
	}
}
