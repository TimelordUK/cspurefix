using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMarketDisruptionFallbacks : IFixGroup
	{
		[TagDetails(Tag = 41095, Type = TagType.String, Offset = 0, Required = false)]
		public string? MarketDisruptionFallbackType {get; set;}
		
		[TagDetails(Tag = 40992, Type = TagType.String, Offset = 1, Required = false)]
		public string? MarketDisruptionFallbackValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MarketDisruptionFallbackType is not null) writer.WriteString(41095, MarketDisruptionFallbackType);
			if (MarketDisruptionFallbackValue is not null) writer.WriteString(40992, MarketDisruptionFallbackValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MarketDisruptionFallbackType = view.GetString(41095);
			MarketDisruptionFallbackValue = view.GetString(40992);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MarketDisruptionFallbackType":
					value = MarketDisruptionFallbackType;
					break;
				case "MarketDisruptionFallbackValue":
					value = MarketDisruptionFallbackValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MarketDisruptionFallbackType = null;
			MarketDisruptionFallbackValue = null;
		}
	}
}
