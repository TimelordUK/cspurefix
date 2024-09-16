using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class StipulationsNoStipulations : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 233, Type = TagType.String, Offset = 0, Required = false)]
		public string? StipulationType { get; set; }
		
		[TagDetails(Tag = 234, Type = TagType.String, Offset = 1, Required = false)]
		public string? StipulationValue { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StipulationType is not null) writer.WriteString(233, StipulationType);
			if (StipulationValue is not null) writer.WriteString(234, StipulationValue);
		}
	}
}
