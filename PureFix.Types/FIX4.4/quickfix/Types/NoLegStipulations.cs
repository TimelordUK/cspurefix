using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoLegStipulations
	{
		[TagDetails(Tag = 688, Type = TagType.String, Offset = 0)]
		public string? LegStipulationType { get; set; }
		
		[TagDetails(Tag = 689, Type = TagType.String, Offset = 1)]
		public string? LegStipulationValue { get; set; }
		
	}
}
