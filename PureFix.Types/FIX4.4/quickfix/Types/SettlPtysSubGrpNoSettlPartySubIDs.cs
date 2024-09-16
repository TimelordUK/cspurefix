using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlPtysSubGrpNoSettlPartySubIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 785, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlPartySubID { get; set; }
		
		[TagDetails(Tag = 786, Type = TagType.Int, Offset = 1, Required = false)]
		public int? SettlPartySubIDType { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SettlPartySubID is not null) writer.WriteString(785, SettlPartySubID);
			if (SettlPartySubIDType is not null) writer.WriteWholeNumber(786, SettlPartySubIDType.Value);
		}
	}
}
