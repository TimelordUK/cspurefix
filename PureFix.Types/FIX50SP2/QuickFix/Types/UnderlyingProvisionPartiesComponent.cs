using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 42173, Offset = 0, Required = false)]
		public IOINoUnderlyingProvisionPartyIDs[]? NoUnderlyingProvisionPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionPartyIDs is not null && NoUnderlyingProvisionPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(42173, NoUnderlyingProvisionPartyIDs.Length);
				for (int i = 0; i < NoUnderlyingProvisionPartyIDs.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionPartyIDs") is IMessageView viewNoUnderlyingProvisionPartyIDs)
			{
				var count = viewNoUnderlyingProvisionPartyIDs.GroupCount();
				NoUnderlyingProvisionPartyIDs = new IOINoUnderlyingProvisionPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionPartyIDs[i] = new();
					((IFixParser)NoUnderlyingProvisionPartyIDs[i]).Parse(viewNoUnderlyingProvisionPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionPartyIDs":
					value = NoUnderlyingProvisionPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProvisionPartyIDs = null;
		}
	}
}
