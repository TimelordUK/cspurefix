using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSecurityTypes
	{
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 0)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 762, Type = TagType.String, Offset = 1)]
		public string? SecuritySubType { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 2)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 3)]
		public string? CFICode { get; set; }
		
	}
}
