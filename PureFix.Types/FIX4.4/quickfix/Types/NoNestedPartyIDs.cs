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
		[TagDetails(524, TagType.String)]
		public string? NestedPartyID { get; set; }
		
		[TagDetails(525, TagType.String)]
		public string? NestedPartyIDSource { get; set; }
		
		[TagDetails(538, TagType.Int)]
		public int? NestedPartyRole { get; set; }
		
		[Component]
		public NstdPtysSubGrp? NstdPtysSubGrp { get; set; }
		
	}
}
