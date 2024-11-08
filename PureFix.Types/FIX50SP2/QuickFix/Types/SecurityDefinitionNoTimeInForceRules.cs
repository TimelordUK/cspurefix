using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityDefinitionNoTimeInForceRules : IFixGroup
	{
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 0, Required = false)]
		public string? TimeInForce {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TimeInForce = view.GetString(59);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TimeInForce":
					value = TimeInForce;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TimeInForce = null;
		}
	}
}
