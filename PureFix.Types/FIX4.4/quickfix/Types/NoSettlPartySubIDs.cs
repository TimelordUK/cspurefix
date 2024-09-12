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
		[TagDetails(Tag = 785, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlPartySubID { get; set; }
		
		[TagDetails(Tag = 786, Type = TagType.Int, Offset = 1, Required = false)]
		public int? SettlPartySubIDType { get; set; }
		
	}
}
