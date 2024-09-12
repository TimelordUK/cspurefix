using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSettlPartySubIDs
	{
		[TagDetails(785, TagType.String)]
		public string? SettlPartySubID { get; set; }
		
		[TagDetails(786, TagType.Int)]
		public int? SettlPartySubIDType { get; set; }
		
	}
}
