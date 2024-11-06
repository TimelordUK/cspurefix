using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyEntitlementAckGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1772, Offset = 0, Required = false)]
		public PartyEntitlementsDefinitionRequestAckNoPartyEntitlements[]? NoPartyEntitlements {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyEntitlements is not null && NoPartyEntitlements.Length != 0)
			{
				writer.WriteWholeNumber(1772, NoPartyEntitlements.Length);
				for (int i = 0; i < NoPartyEntitlements.Length; i++)
				{
					((IFixEncoder)NoPartyEntitlements[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyEntitlements") is IMessageView viewNoPartyEntitlements)
			{
				var count = viewNoPartyEntitlements.GroupCount();
				NoPartyEntitlements = new PartyEntitlementsDefinitionRequestAckNoPartyEntitlements[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyEntitlements[i] = new();
					((IFixParser)NoPartyEntitlements[i]).Parse(viewNoPartyEntitlements.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyEntitlements":
					value = NoPartyEntitlements;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPartyEntitlements = null;
		}
	}
}
