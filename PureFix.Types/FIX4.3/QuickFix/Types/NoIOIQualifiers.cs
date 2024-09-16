using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class NoIOIQualifiers : IFixGroup
	{
		[TagDetails(Tag = 104, Type = TagType.String, Offset = 0, Required = false)]
		public string? IOIQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (IOIQualifier is not null) writer.WriteString(104, IOIQualifier);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			IOIQualifier = view.GetString(104);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "IOIQualifier":
					value = IOIQualifier;
					break;
				default: return false;
			}
			return true;
		}
	}
}
