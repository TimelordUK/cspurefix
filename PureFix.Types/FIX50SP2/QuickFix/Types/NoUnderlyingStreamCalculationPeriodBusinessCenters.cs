using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingStreamCalculationPeriodBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40557, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStreamCalculationPeriodBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamCalculationPeriodBusinessCenter is not null) writer.WriteString(40557, UnderlyingStreamCalculationPeriodBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamCalculationPeriodBusinessCenter = view.GetString(40557);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamCalculationPeriodBusinessCenter":
					value = UnderlyingStreamCalculationPeriodBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStreamCalculationPeriodBusinessCenter = null;
		}
	}
}
