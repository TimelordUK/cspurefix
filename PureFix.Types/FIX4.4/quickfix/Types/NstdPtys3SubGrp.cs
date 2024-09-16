using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtys3SubGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 952, Offset = 0, Required = false)]
		public NstdPtys3SubGrpNoNested3PartySubIDs[]? NoNested3PartySubIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested3PartySubIDs is not null && NoNested3PartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(952, NoNested3PartySubIDs.Length);
				for (int i = 0; i < NoNested3PartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNested3PartySubIDs[i]).Encode(writer);
				}
			}
		}
	}
}
