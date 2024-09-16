using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class AllocationNoAllocsNoMiscFees : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 137, Type = TagType.Float, Offset = 0, Required = false)]
		public double? MiscFeeAmt { get; set; }
		
		[TagDetails(Tag = 138, Type = TagType.String, Offset = 1, Required = false)]
		public string? MiscFeeCurr { get; set; }
		
		[TagDetails(Tag = 139, Type = TagType.String, Offset = 2, Required = false)]
		public string? MiscFeeType { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MiscFeeAmt is not null) writer.WriteNumber(137, MiscFeeAmt.Value);
			if (MiscFeeCurr is not null) writer.WriteString(138, MiscFeeCurr);
			if (MiscFeeType is not null) writer.WriteString(139, MiscFeeType);
		}
	}
}
