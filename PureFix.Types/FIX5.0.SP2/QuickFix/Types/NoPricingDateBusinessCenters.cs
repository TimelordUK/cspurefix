using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPricingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41231, Type = TagType.String, Offset = 0, Required = false)]
		public string? PricingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PricingDateBusinessCenter is not null) writer.WriteString(41231, PricingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PricingDateBusinessCenter = view.GetString(41231);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PricingDateBusinessCenter":
					value = PricingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
