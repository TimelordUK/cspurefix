using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NestedParties2NoNested2PartyIDs
	{
		[TagDetails(Tag = 757, Type = TagType.String, Offset = 0, Required = false)]
		public string? Nested2PartyID { get; set; }
		
		[TagDetails(Tag = 758, Type = TagType.String, Offset = 1, Required = false)]
		public string? Nested2PartyIDSource { get; set; }
		
		[TagDetails(Tag = 759, Type = TagType.Int, Offset = 2, Required = false)]
		public int? Nested2PartyRole { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public NstdPtys2SubGrp? NstdPtys2SubGrp { get; set; }
		
	}
}
