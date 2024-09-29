using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMarketDisruptionEvents : IFixGroup
	{
		[TagDetails(Tag = 41093, Type = TagType.String, Offset = 0, Required = false)]
		public string? MarketDisruptionEvent {get; set;}
		
		[TagDetails(Tag = 40991, Type = TagType.String, Offset = 1, Required = false)]
		public string? MarketDisruptionValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MarketDisruptionEvent is not null) writer.WriteString(41093, MarketDisruptionEvent);
			if (MarketDisruptionValue is not null) writer.WriteString(40991, MarketDisruptionValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MarketDisruptionEvent = view.GetString(41093);
			MarketDisruptionValue = view.GetString(40991);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MarketDisruptionEvent":
					value = MarketDisruptionEvent;
					break;
				case "MarketDisruptionValue":
					value = MarketDisruptionValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MarketDisruptionEvent = null;
			MarketDisruptionValue = null;
		}
	}
}
