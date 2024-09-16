using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedParties : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 539, Offset = 0, Required = false)]
		public NestedPartiesNoNestedPartyIDs[]? NoNestedPartyIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNestedPartyIDs is not null && NoNestedPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(539, NoNestedPartyIDs.Length);
				for (int i = 0; i < NoNestedPartyIDs.Length; i++)
				{
					((IFixEncoder)NoNestedPartyIDs[i]).Encode(writer);
				}
			}
		}
	}
}
