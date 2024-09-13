using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class PartiesNoPartyIDs
	{
		[TagDetails(Tag = 448, Type = TagType.String, Offset = 0, Required = false)]
		public string? PartyID { get; set; }
		
		[TagDetails(Tag = 447, Type = TagType.String, Offset = 1, Required = false)]
		public string? PartyIDSource { get; set; }
		
		[TagDetails(Tag = 452, Type = TagType.Int, Offset = 2, Required = false)]
		public int? PartyRole { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public PtysSubGrp? PtysSubGrp { get; set; }
		
	}
}
