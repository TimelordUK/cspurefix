using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class ClrInstGrpNoClearingInstructions
	{
		[TagDetails(Tag = 577, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ClearingInstruction { get; set; }
		
	}
}
