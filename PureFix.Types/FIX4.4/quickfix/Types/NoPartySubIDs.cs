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
		[TagDetails(523, TagType.String)]
		public string? PartySubID { get; set; }
		
		[TagDetails(803, TagType.Int)]
		public int? PartySubIDType { get; set; }
		
	}
}
