using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegDividendPeriodBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42387, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegDividendPeriodBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegDividendPeriodBusinessCenter is not null) writer.WriteString(42387, LegDividendPeriodBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegDividendPeriodBusinessCenter = view.GetString(42387);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegDividendPeriodBusinessCenter":
					value = LegDividendPeriodBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
