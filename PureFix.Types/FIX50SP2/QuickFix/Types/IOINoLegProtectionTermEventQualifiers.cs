using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegProtectionTermEventQualifiers : IFixGroup
	{
		[TagDetails(Tag = 41634, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProtectionTermEventQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProtectionTermEventQualifier is not null) writer.WriteString(41634, LegProtectionTermEventQualifier);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProtectionTermEventQualifier = view.GetString(41634);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProtectionTermEventQualifier":
					value = LegProtectionTermEventQualifier;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegProtectionTermEventQualifier = null;
		}
	}
}
