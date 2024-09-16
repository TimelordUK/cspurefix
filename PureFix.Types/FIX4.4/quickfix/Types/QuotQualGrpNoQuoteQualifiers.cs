using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotQualGrpNoQuoteQualifiers : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 695, Type = TagType.String, Offset = 0, Required = false)]
		public string? QuoteQualifier { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteQualifier is not null) writer.WriteString(695, QuoteQualifier);
		}
	}
}
