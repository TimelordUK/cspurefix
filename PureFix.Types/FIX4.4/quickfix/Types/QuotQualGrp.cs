using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotQualGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 735, Offset = 0, Required = false)]
		public QuotQualGrpNoQuoteQualifiers[]? NoQuoteQualifiers { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteQualifiers is not null && NoQuoteQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(735, NoQuoteQualifiers.Length);
				for (int i = 0; i < NoQuoteQualifiers.Length; i++)
				{
					((IFixEncoder)NoQuoteQualifiers[i]).Encode(writer);
				}
			}
		}
	}
}
