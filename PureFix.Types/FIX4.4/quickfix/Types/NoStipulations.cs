using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoStipulations
	{
		[TagDetails(Tag = 233, Type = TagType.String, Offset = 0, Required = false)]
		public string? StipulationType { get; set; }
		
		[TagDetails(Tag = 234, Type = TagType.String, Offset = 1, Required = false)]
		public string? StipulationValue { get; set; }
		
	}
}
