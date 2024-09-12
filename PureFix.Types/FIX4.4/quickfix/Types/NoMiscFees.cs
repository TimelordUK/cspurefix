using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoMiscFees
	{
		[TagDetails(137, TagType.Float)]
		public double? MiscFeeAmt { get; set; }
		
		[TagDetails(138, TagType.String)]
		public string? MiscFeeCurr { get; set; }
		
		[TagDetails(139, TagType.String)]
		public string? MiscFeeType { get; set; }
		
		[TagDetails(891, TagType.Int)]
		public int? MiscFeeBasis { get; set; }
		
	}
}
