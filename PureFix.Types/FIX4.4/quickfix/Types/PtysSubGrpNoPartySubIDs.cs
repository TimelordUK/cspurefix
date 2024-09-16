using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PtysSubGrpNoPartySubIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 523, Type = TagType.String, Offset = 0, Required = false)]
		public string? PartySubID { get; set; }
		
		[TagDetails(Tag = 803, Type = TagType.Int, Offset = 1, Required = false)]
		public int? PartySubIDType { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PartySubID is not null) writer.WriteString(523, PartySubID);
			if (PartySubIDType is not null) writer.WriteWholeNumber(803, PartySubIDType.Value);
		}
	}
}
