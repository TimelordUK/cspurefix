using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoPartySubIDs
	{
		[TagDetails(Tag = 523, Type = TagType.String, Offset = 0)]
		public string? PartySubID { get; set; }
		
		[TagDetails(Tag = 803, Type = TagType.Int, Offset = 1)]
		public int? PartySubIDType { get; set; }
		
	}
}
