using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegMarketDisruptionEvents : IFixGroup
	{
		[TagDetails(Tag = 41468, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegMarketDisruptionEvent {get; set;}
		
		[TagDetails(Tag = 40223, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegMarketDisruptionValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegMarketDisruptionEvent is not null) writer.WriteString(41468, LegMarketDisruptionEvent);
			if (LegMarketDisruptionValue is not null) writer.WriteString(40223, LegMarketDisruptionValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegMarketDisruptionEvent = view.GetString(41468);
			LegMarketDisruptionValue = view.GetString(40223);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegMarketDisruptionEvent":
					value = LegMarketDisruptionEvent;
					break;
				case "LegMarketDisruptionValue":
					value = LegMarketDisruptionValue;
					break;
				default: return false;
			}
			return true;
		}
	}
}
