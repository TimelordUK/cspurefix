using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class SideCrossOrdCxlGrp
	{
		[Group(552)]
		public NoSides[]? NoSides { get; set; }
		
	}
}
