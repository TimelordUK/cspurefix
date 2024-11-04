using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegComplexEventCreditEventSources : IFixGroup
	{
		[TagDetails(Tag = 41399, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegComplexEventCreditEventSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegComplexEventCreditEventSource is not null) writer.WriteString(41399, LegComplexEventCreditEventSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegComplexEventCreditEventSource = view.GetString(41399);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegComplexEventCreditEventSource":
					value = LegComplexEventCreditEventSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegComplexEventCreditEventSource = null;
		}
	}
}
