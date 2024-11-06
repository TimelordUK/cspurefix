using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityDefinitionNoOrdTypeRules : IFixGroup
	{
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 0, Required = false)]
		public string? OrdType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (OrdType is not null) writer.WriteString(40, OrdType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OrdType = view.GetString(40);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "OrdType":
					value = OrdType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			OrdType = null;
		}
	}
}
