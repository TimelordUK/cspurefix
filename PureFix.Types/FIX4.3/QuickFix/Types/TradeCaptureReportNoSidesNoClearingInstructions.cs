using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoSidesNoClearingInstructions : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 577, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ClearingInstruction { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClearingInstruction is not null) writer.WriteWholeNumber(577, ClearingInstruction.Value);
		}
	}
}
