using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUsernames : IFixGroup
	{
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 0, Required = false)]
		public string? Username {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Username is not null) writer.WriteString(553, Username);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Username = view.GetString(553);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Username":
					value = Username;
					break;
				default: return false;
			}
			return true;
		}
	}
}
