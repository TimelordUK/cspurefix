using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoNestedPartySubIDs
	{
		[TagDetails(Tag = 545, Type = TagType.String, Offset = 0)]
		public string? NestedPartySubID { get; set; }
		
		[TagDetails(Tag = 805, Type = TagType.Int, Offset = 1)]
		public int? NestedPartySubIDType { get; set; }
		
	}
}
