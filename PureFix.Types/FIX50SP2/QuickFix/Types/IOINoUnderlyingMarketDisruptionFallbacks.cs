using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingMarketDisruptionFallbacks : IFixGroup
	{
		[TagDetails(Tag = 41867, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingMarketDisruptionFallbackType {get; set;}
		
		[TagDetails(Tag = 41339, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingMarketDisruptionFallbackValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingMarketDisruptionFallbackType is not null) writer.WriteString(41867, UnderlyingMarketDisruptionFallbackType);
			if (UnderlyingMarketDisruptionFallbackValue is not null) writer.WriteString(41339, UnderlyingMarketDisruptionFallbackValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingMarketDisruptionFallbackType = view.GetString(41867);
			UnderlyingMarketDisruptionFallbackValue = view.GetString(41339);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingMarketDisruptionFallbackType":
					value = UnderlyingMarketDisruptionFallbackType;
					break;
				case "UnderlyingMarketDisruptionFallbackValue":
					value = UnderlyingMarketDisruptionFallbackValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingMarketDisruptionFallbackType = null;
			UnderlyingMarketDisruptionFallbackValue = null;
		}
	}
}
