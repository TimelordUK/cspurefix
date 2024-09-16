using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtysSubGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 804, Offset = 0, Required = false)]
		public NstdPtysSubGrpNoNestedPartySubIDs[]? NoNestedPartySubIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNestedPartySubIDs is not null && NoNestedPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(804, NoNestedPartySubIDs.Length);
				for (int i = 0; i < NoNestedPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNestedPartySubIDs[i]).Encode(writer);
				}
			}
		}
	}
}
