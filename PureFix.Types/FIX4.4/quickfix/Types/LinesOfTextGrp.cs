using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class LinesOfTextGrp
	{
		[Group(NoOfTag = 33, Offset = 0, Required = true)]
		public NoLinesOfText[]? NoLinesOfText { get; set; }
		
	}
}
