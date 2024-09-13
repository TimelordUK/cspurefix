using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class IOIQualGrpNoIOIQualifiers
	{
		[TagDetails(Tag = 104, Type = TagType.String, Offset = 0, Required = false)]
		public string? IOIQualifier { get; set; }
		
	}
}
