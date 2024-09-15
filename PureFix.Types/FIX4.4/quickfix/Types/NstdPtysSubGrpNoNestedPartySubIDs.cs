using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtysSubGrpNoNestedPartySubIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 545, Type = TagType.String, Offset = 0, Required = false)]
		public string? NestedPartySubID { get; set; }
		
		[TagDetails(Tag = 805, Type = TagType.Int, Offset = 1, Required = false)]
		public int? NestedPartySubIDType { get; set; }
		
	}
}
