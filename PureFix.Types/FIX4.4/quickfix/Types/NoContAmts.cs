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
		[TagDetails(519, TagType.Int)]
		public int? ContAmtType { get; set; }
		
		[TagDetails(520, TagType.Float)]
		public double? ContAmtValue { get; set; }
		
		[TagDetails(521, TagType.String)]
		public string? ContAmtCurr { get; set; }
		
	}
}
