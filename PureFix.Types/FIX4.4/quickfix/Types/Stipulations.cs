using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class Stipulations
	{
		[Group(NoOfTag = 232, Offset = 0, Required = false)]
		public NoStipulations[]? NoStipulations { get; set; }
		
	}
}
