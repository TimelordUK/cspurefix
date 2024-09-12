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
		[TagDetails(888, TagType.String)]
		public string? UnderlyingStipType { get; set; }
		
		[TagDetails(889, TagType.String)]
		public string? UnderlyingStipValue { get; set; }
		
	}
}
