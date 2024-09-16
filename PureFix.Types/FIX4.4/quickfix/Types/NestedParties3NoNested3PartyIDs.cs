using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedParties3NoNested3PartyIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 949, Type = TagType.String, Offset = 0, Required = false)]
		public string? Nested3PartyID { get; set; }
		
		[TagDetails(Tag = 950, Type = TagType.String, Offset = 1, Required = false)]
		public string? Nested3PartyIDSource { get; set; }
		
		[TagDetails(Tag = 951, Type = TagType.Int, Offset = 2, Required = false)]
		public int? Nested3PartyRole { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public NstdPtys3SubGrp? NstdPtys3SubGrp { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Nested3PartyID is not null) writer.WriteString(949, Nested3PartyID);
			if (Nested3PartyIDSource is not null) writer.WriteString(950, Nested3PartyIDSource);
			if (Nested3PartyRole is not null) writer.WriteWholeNumber(951, Nested3PartyRole.Value);
			if (NstdPtys3SubGrp is not null) ((IFixEncoder)NstdPtys3SubGrp).Encode(writer);
		}
	}
}
