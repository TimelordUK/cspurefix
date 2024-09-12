using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoUnderlyingStips
	{
		[TagDetails(Tag = 888, Type = TagType.String, Offset = 0)]
		public string? UnderlyingStipType { get; set; }
		
		[TagDetails(Tag = 889, Type = TagType.String, Offset = 1)]
		public string? UnderlyingStipValue { get; set; }
		
	}
}
