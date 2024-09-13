using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class MiscFeesGrpNoMiscFees
	{
		[TagDetails(Tag = 137, Type = TagType.Float, Offset = 0, Required = false)]
		public double? MiscFeeAmt { get; set; }
		
		[TagDetails(Tag = 138, Type = TagType.String, Offset = 1, Required = false)]
		public string? MiscFeeCurr { get; set; }
		
		[TagDetails(Tag = 139, Type = TagType.String, Offset = 2, Required = false)]
		public string? MiscFeeType { get; set; }
		
		[TagDetails(Tag = 891, Type = TagType.Int, Offset = 3, Required = false)]
		public int? MiscFeeBasis { get; set; }
		
	}
}
