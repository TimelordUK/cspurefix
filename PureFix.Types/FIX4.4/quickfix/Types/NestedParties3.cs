using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedParties3 : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 948, Offset = 0, Required = false)]
		public NestedParties3NoNested3PartyIDs[]? NoNested3PartyIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested3PartyIDs is not null && NoNested3PartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(948, NoNested3PartyIDs.Length);
				for (int i = 0; i < NoNested3PartyIDs.Length; i++)
				{
					((IFixEncoder)NoNested3PartyIDs[i]).Encode(writer);
				}
			}
		}
	}
}
