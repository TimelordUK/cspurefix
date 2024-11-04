using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RequestedPartyRoleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1508, Offset = 0, Required = false)]
		public NoRequestedPartyRoles[]? NoRequestedPartyRoles {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRequestedPartyRoles is not null && NoRequestedPartyRoles.Length != 0)
			{
				writer.WriteWholeNumber(1508, NoRequestedPartyRoles.Length);
				for (int i = 0; i < NoRequestedPartyRoles.Length; i++)
				{
					((IFixEncoder)NoRequestedPartyRoles[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRequestedPartyRoles") is IMessageView viewNoRequestedPartyRoles)
			{
				var count = viewNoRequestedPartyRoles.GroupCount();
				NoRequestedPartyRoles = new NoRequestedPartyRoles[count];
				for (int i = 0; i < count; i++)
				{
					NoRequestedPartyRoles[i] = new();
					((IFixParser)NoRequestedPartyRoles[i]).Parse(viewNoRequestedPartyRoles.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRequestedPartyRoles":
					value = NoRequestedPartyRoles;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRequestedPartyRoles = null;
		}
	}
}
