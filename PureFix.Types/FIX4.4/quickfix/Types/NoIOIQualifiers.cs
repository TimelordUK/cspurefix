using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoIOIQualifiers
	{
		[TagDetails(Tag = 104, Type = TagType.String, Offset = 0)]
		public string? IOIQualifier { get; set; }
		
	}
}
