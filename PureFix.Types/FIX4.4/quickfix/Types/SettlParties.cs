using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlParties : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 781, Offset = 0, Required = false)]
		public SettlPartiesNoSettlPartyIDs[]? NoSettlPartyIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlPartyIDs is not null && NoSettlPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(781, NoSettlPartyIDs.Length);
				for (int i = 0; i < NoSettlPartyIDs.Length; i++)
				{
					((IFixEncoder)NoSettlPartyIDs[i]).Encode(writer);
				}
			}
		}
	}
}
