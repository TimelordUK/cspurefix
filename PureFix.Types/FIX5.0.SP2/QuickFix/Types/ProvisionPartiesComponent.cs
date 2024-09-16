using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 40174, Offset = 0, Required = false)]
		public NoProvisionPartyIDs[]? NoProvisionPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionPartyIDs is not null && NoProvisionPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(40174, NoProvisionPartyIDs.Length);
				for (int i = 0; i < NoProvisionPartyIDs.Length; i++)
				{
					((IFixEncoder)NoProvisionPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionPartyIDs") is IMessageView viewNoProvisionPartyIDs)
			{
				var count = viewNoProvisionPartyIDs.GroupCount();
				NoProvisionPartyIDs = new NoProvisionPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionPartyIDs[i] = new();
					((IFixParser)NoProvisionPartyIDs[i]).Parse(viewNoProvisionPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionPartyIDs":
					value = NoProvisionPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
