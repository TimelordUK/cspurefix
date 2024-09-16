using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtys2SubGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 806, Offset = 0, Required = false)]
		public NstdPtys2SubGrpNoNested2PartySubIDs[]? NoNested2PartySubIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested2PartySubIDs is not null && NoNested2PartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(806, NoNested2PartySubIDs.Length);
				for (int i = 0; i < NoNested2PartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNested2PartySubIDs[i]).Encode(writer);
				}
			}
		}
	}
}
