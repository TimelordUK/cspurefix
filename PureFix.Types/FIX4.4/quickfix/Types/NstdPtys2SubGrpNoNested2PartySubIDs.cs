using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtys2SubGrpNoNested2PartySubIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 760, Type = TagType.String, Offset = 0, Required = false)]
		public string? Nested2PartySubID { get; set; }
		
		[TagDetails(Tag = 807, Type = TagType.Int, Offset = 1, Required = false)]
		public int? Nested2PartySubIDType { get; set; }
		
	}
}
