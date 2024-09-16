using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class PartiesNoPartyIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 448, Type = TagType.String, Offset = 0, Required = false)]
		public string? PartyID { get; set; }
		
		[TagDetails(Tag = 447, Type = TagType.String, Offset = 1, Required = false)]
		public string? PartyIDSource { get; set; }
		
		[TagDetails(Tag = 452, Type = TagType.Int, Offset = 2, Required = false)]
		public int? PartyRole { get; set; }
		
		[TagDetails(Tag = 523, Type = TagType.String, Offset = 3, Required = false)]
		public string? PartySubID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PartyID is not null) writer.WriteString(448, PartyID);
			if (PartyIDSource is not null) writer.WriteString(447, PartyIDSource);
			if (PartyRole is not null) writer.WriteWholeNumber(452, PartyRole.Value);
			if (PartySubID is not null) writer.WriteString(523, PartySubID);
		}
	}
}
