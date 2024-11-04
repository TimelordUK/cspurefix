using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 40533, Offset = 0, Required = false)]
		public NoLegProvisionPartyIDs[]? NoLegProvisionPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionPartyIDs is not null && NoLegProvisionPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(40533, NoLegProvisionPartyIDs.Length);
				for (int i = 0; i < NoLegProvisionPartyIDs.Length; i++)
				{
					((IFixEncoder)NoLegProvisionPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionPartyIDs") is IMessageView viewNoLegProvisionPartyIDs)
			{
				var count = viewNoLegProvisionPartyIDs.GroupCount();
				NoLegProvisionPartyIDs = new NoLegProvisionPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionPartyIDs[i] = new();
					((IFixParser)NoLegProvisionPartyIDs[i]).Parse(viewNoLegProvisionPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionPartyIDs":
					value = NoLegProvisionPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProvisionPartyIDs = null;
		}
	}
}
