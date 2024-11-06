using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class IOINoLegStipulations : IFixGroup
	{
		[TagDetails(Tag = 688, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegStipulationType {get; set;}
		
		[TagDetails(Tag = 689, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegStipulationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegStipulationType is not null) writer.WriteString(688, LegStipulationType);
			if (LegStipulationValue is not null) writer.WriteString(689, LegStipulationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegStipulationType = view.GetString(688);
			LegStipulationValue = view.GetString(689);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegStipulationType":
					value = LegStipulationType;
					break;
				case "LegStipulationValue":
					value = LegStipulationValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegStipulationType = null;
			LegStipulationValue = null;
		}
	}
}
