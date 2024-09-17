using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegExtraordinaryEvents : IFixGroup
	{
		[TagDetails(Tag = 42389, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegExtraordinaryEventType {get; set;}
		
		[TagDetails(Tag = 42390, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegExtraordinaryEventValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegExtraordinaryEventType is not null) writer.WriteString(42389, LegExtraordinaryEventType);
			if (LegExtraordinaryEventValue is not null) writer.WriteString(42390, LegExtraordinaryEventValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegExtraordinaryEventType = view.GetString(42389);
			LegExtraordinaryEventValue = view.GetString(42390);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegExtraordinaryEventType":
					value = LegExtraordinaryEventType;
					break;
				case "LegExtraordinaryEventValue":
					value = LegExtraordinaryEventValue;
					break;
				default: return false;
			}
			return true;
		}
	}
}
