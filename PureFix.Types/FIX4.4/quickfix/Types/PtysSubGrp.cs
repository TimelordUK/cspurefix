using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PtysSubGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 802, Offset = 0, Required = false)]
		public PtysSubGrpNoPartySubIDs[]? NoPartySubIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartySubIDs is not null && NoPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(802, NoPartySubIDs.Length);
				for (int i = 0; i < NoPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoPartySubIDs[i]).Encode(writer);
				}
			}
		}
	}
}
