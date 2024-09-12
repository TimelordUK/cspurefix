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
		[TagDetails(Tag = 524, Type = TagType.String, Offset = 0, Required = false)]
		public string? NestedPartyID { get; set; }
		
		[TagDetails(Tag = 525, Type = TagType.String, Offset = 1, Required = false)]
		public string? NestedPartyIDSource { get; set; }
		
		[TagDetails(Tag = 538, Type = TagType.Int, Offset = 2, Required = false)]
		public int? NestedPartyRole { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public NstdPtysSubGrp? NstdPtysSubGrp { get; set; }
		
	}
}
