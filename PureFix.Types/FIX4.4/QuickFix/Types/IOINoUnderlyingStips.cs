using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingStips : IFixGroup
	{
		[TagDetails(Tag = 888, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStipType {get; set;}
		
		[TagDetails(Tag = 889, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingStipValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStipType is not null) writer.WriteString(888, UnderlyingStipType);
			if (UnderlyingStipValue is not null) writer.WriteString(889, UnderlyingStipValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStipType = view.GetString(888);
			UnderlyingStipValue = view.GetString(889);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStipType":
					value = UnderlyingStipType;
					break;
				case "UnderlyingStipValue":
					value = UnderlyingStipValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStipType = null;
			UnderlyingStipValue = null;
		}
	}
}
