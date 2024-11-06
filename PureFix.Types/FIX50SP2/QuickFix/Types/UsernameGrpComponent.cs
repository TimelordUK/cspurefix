using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UsernameGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 809, Offset = 0, Required = false)]
		public UserNotificationNoUsernames[]? NoUsernames {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUsernames is not null && NoUsernames.Length != 0)
			{
				writer.WriteWholeNumber(809, NoUsernames.Length);
				for (int i = 0; i < NoUsernames.Length; i++)
				{
					((IFixEncoder)NoUsernames[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUsernames") is IMessageView viewNoUsernames)
			{
				var count = viewNoUsernames.GroupCount();
				NoUsernames = new UserNotificationNoUsernames[count];
				for (int i = 0; i < count; i++)
				{
					NoUsernames[i] = new();
					((IFixParser)NoUsernames[i]).Parse(viewNoUsernames.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUsernames":
					value = NoUsernames;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUsernames = null;
		}
	}
}
