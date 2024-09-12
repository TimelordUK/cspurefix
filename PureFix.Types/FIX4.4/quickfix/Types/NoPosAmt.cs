using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPosAmt
	{
		[TagDetails(Tag = 707, Type = TagType.String, Offset = 0)]
		public string? PosAmtType { get; set; }
		
		[TagDetails(Tag = 708, Type = TagType.Float, Offset = 1)]
		public double? PosAmt { get; set; }
		
	}
}
