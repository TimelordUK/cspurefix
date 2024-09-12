using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPartyIDs
	{
		[TagDetails(448, TagType.String)]
		public string? PartyID { get; set; }
		
		[TagDetails(447, TagType.String)]
		public string? PartyIDSource { get; set; }
		
		[TagDetails(452, TagType.Int)]
		public int? PartyRole { get; set; }
		
		[Component]
		public PtysSubGrp? PtysSubGrp { get; set; }
		
	}
}
