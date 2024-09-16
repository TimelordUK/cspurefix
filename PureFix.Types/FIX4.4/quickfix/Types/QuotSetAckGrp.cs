using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotSetAckGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 296, Offset = 0, Required = false)]
		public QuotSetAckGrpNoQuoteSets[]? NoQuoteSets { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteSets is not null && NoQuoteSets.Length != 0)
			{
				writer.WriteWholeNumber(296, NoQuoteSets.Length);
				for (int i = 0; i < NoQuoteSets.Length; i++)
				{
					((IFixEncoder)NoQuoteSets[i]).Encode(writer);
				}
			}
		}
	}
}
