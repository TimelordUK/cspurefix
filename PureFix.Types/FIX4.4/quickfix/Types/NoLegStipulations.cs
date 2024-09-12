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
		[TagDetails(688, TagType.String)]
		public string? LegStipulationType { get; set; }
		
		[TagDetails(689, TagType.String)]
		public string? LegStipulationValue { get; set; }
		
	}
}
