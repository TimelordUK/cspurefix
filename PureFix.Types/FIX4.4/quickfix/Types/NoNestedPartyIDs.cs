using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoNestedPartyIDs
	{
		[TagDetails(Tag = 524, Type = TagType.String, Offset = 0)]
		public string? NestedPartyID { get; set; }
		
		[TagDetails(Tag = 525, Type = TagType.String, Offset = 1)]
		public string? NestedPartyIDSource { get; set; }
		
		[TagDetails(Tag = 538, Type = TagType.Int, Offset = 2)]
		public int? NestedPartyRole { get; set; }
		
		[Component(Offset = 3)]
		public NstdPtysSubGrp? NstdPtysSubGrp { get; set; }
		
	}
}
