using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegDividendFXTriggerDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42365, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegDividendFXTriggerDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegDividendFXTriggerDateBusinessCenter is not null) writer.WriteString(42365, LegDividendFXTriggerDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegDividendFXTriggerDateBusinessCenter = view.GetString(42365);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegDividendFXTriggerDateBusinessCenter":
					value = LegDividendFXTriggerDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
