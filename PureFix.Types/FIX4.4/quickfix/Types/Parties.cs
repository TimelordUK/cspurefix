using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class Parties : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 453, Offset = 0, Required = false)]
		public PartiesNoPartyIDs[]? NoPartyIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyIDs is not null && NoPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(453, NoPartyIDs.Length);
				for (int i = 0; i < NoPartyIDs.Length; i++)
				{
					((IFixEncoder)NoPartyIDs[i]).Encode(writer);
				}
			}
		}
	}
}
