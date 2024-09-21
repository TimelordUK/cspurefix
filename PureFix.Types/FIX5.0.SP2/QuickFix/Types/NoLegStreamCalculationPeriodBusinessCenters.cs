using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegStreamCalculationPeriodBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40266, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegStreamCalculationPeriodBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegStreamCalculationPeriodBusinessCenter is not null) writer.WriteString(40266, LegStreamCalculationPeriodBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegStreamCalculationPeriodBusinessCenter = view.GetString(40266);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegStreamCalculationPeriodBusinessCenter":
					value = LegStreamCalculationPeriodBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegStreamCalculationPeriodBusinessCenter = null;
		}
	}
}
