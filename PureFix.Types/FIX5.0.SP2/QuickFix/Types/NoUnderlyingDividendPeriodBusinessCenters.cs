using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingDividendPeriodBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42883, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingDividendPeriodBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDividendPeriodBusinessCenter is not null) writer.WriteString(42883, UnderlyingDividendPeriodBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDividendPeriodBusinessCenter = view.GetString(42883);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDividendPeriodBusinessCenter":
					value = UnderlyingDividendPeriodBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingDividendPeriodBusinessCenter = null;
		}
	}
}
