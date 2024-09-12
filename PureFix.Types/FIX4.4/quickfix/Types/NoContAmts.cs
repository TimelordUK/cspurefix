using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoContAmts
	{
		[TagDetails(Tag = 519, Type = TagType.Int, Offset = 0)]
		public int? ContAmtType { get; set; }
		
		[TagDetails(Tag = 520, Type = TagType.Float, Offset = 1)]
		public double? ContAmtValue { get; set; }
		
		[TagDetails(Tag = 521, Type = TagType.String, Offset = 2)]
		public string? ContAmtCurr { get; set; }
		
	}
}
