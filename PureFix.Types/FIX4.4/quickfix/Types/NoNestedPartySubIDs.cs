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
		[TagDetails(545, TagType.String)]
		public string? NestedPartySubID { get; set; }
		
		[TagDetails(805, TagType.Int)]
		public int? NestedPartySubIDType { get; set; }
		
	}
}
