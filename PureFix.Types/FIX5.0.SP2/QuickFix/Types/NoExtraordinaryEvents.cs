using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoExtraordinaryEvents : IFixGroup
	{
		[TagDetails(Tag = 42297, Type = TagType.String, Offset = 0, Required = false)]
		public string? ExtraordinaryEventType {get; set;}
		
		[TagDetails(Tag = 42298, Type = TagType.String, Offset = 1, Required = false)]
		public string? ExtraordinaryEventValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ExtraordinaryEventType is not null) writer.WriteString(42297, ExtraordinaryEventType);
			if (ExtraordinaryEventValue is not null) writer.WriteString(42298, ExtraordinaryEventValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ExtraordinaryEventType = view.GetString(42297);
			ExtraordinaryEventValue = view.GetString(42298);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ExtraordinaryEventType":
					value = ExtraordinaryEventType;
					break;
				case "ExtraordinaryEventValue":
					value = ExtraordinaryEventValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ExtraordinaryEventType = null;
			ExtraordinaryEventValue = null;
		}
	}
}
