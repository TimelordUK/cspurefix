using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotEntryGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 295, Offset = 0, Required = true)]
		public QuotEntryGrpNoQuoteEntries[]? NoQuoteEntries { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoQuoteEntries is not null && FixValidator.IsValid(NoQuoteEntries, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteEntries is not null && NoQuoteEntries.Length != 0)
			{
				writer.WriteWholeNumber(295, NoQuoteEntries.Length);
				for (int i = 0; i < NoQuoteEntries.Length; i++)
				{
					((IFixEncoder)NoQuoteEntries[i]).Encode(writer);
				}
			}
		}
	}
}
