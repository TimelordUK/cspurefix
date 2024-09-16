using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 146, Offset = 0, Required = false)]
		public InstrmtGrpNoRelatedSym[]? NoRelatedSym { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedSym is not null && NoRelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, NoRelatedSym.Length);
				for (int i = 0; i < NoRelatedSym.Length; i++)
				{
					((IFixEncoder)NoRelatedSym[i]).Encode(writer);
				}
			}
		}
	}
}
