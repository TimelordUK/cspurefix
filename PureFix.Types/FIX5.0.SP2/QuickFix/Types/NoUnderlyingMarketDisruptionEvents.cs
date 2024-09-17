using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingMarketDisruptionEvents : IFixGroup
	{
		[TagDetails(Tag = 41865, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingMarketDisruptionEvent {get; set;}
		
		[TagDetails(Tag = 41338, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingMarketDisruptionValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingMarketDisruptionEvent is not null) writer.WriteString(41865, UnderlyingMarketDisruptionEvent);
			if (UnderlyingMarketDisruptionValue is not null) writer.WriteString(41338, UnderlyingMarketDisruptionValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingMarketDisruptionEvent = view.GetString(41865);
			UnderlyingMarketDisruptionValue = view.GetString(41338);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingMarketDisruptionEvent":
					value = UnderlyingMarketDisruptionEvent;
					break;
				case "UnderlyingMarketDisruptionValue":
					value = UnderlyingMarketDisruptionValue;
					break;
				default: return false;
			}
			return true;
		}
	}
}
