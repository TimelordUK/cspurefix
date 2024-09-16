using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ContAmtGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 518, Offset = 0, Required = false)]
		public ContAmtGrpNoContAmts[]? NoContAmts { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoContAmts is not null && NoContAmts.Length != 0)
			{
				writer.WriteWholeNumber(518, NoContAmts.Length);
				for (int i = 0; i < NoContAmts.Length; i++)
				{
					((IFixEncoder)NoContAmts[i]).Encode(writer);
				}
			}
		}
	}
}
