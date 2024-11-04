using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoProtectionTermEventQualifiers : IFixGroup
	{
		[TagDetails(Tag = 40200, Type = TagType.String, Offset = 0, Required = false)]
		public string? ProtectionTermEventQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProtectionTermEventQualifier is not null) writer.WriteString(40200, ProtectionTermEventQualifier);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProtectionTermEventQualifier = view.GetString(40200);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProtectionTermEventQualifier":
					value = ProtectionTermEventQualifier;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ProtectionTermEventQualifier = null;
		}
	}
}
