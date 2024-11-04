using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingPricingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41948, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingPricingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPricingDateBusinessCenter is not null) writer.WriteString(41948, UnderlyingPricingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPricingDateBusinessCenter = view.GetString(41948);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPricingDateBusinessCenter":
					value = UnderlyingPricingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingPricingDateBusinessCenter = null;
		}
	}
}
