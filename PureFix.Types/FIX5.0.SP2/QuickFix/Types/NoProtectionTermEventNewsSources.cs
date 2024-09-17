using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoProtectionTermEventNewsSources : IFixGroup
	{
		[TagDetails(Tag = 40189, Type = TagType.String, Offset = 0, Required = false)]
		public string? ProtectionTermEventNewsSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProtectionTermEventNewsSource is not null) writer.WriteString(40189, ProtectionTermEventNewsSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProtectionTermEventNewsSource = view.GetString(40189);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProtectionTermEventNewsSource":
					value = ProtectionTermEventNewsSource;
					break;
				default: return false;
			}
			return true;
		}
	}
}
