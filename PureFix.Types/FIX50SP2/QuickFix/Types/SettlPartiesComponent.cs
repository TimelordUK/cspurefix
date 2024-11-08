using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SettlPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 781, Offset = 0, Required = false)]
		public AllocationInstructionNoSettlPartyIDs[]? NoSettlPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlPartyIDs is not null && NoSettlPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(781, NoSettlPartyIDs.Length);
				for (int i = 0; i < NoSettlPartyIDs.Length; i++)
				{
					((IFixEncoder)NoSettlPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlPartyIDs") is IMessageView viewNoSettlPartyIDs)
			{
				var count = viewNoSettlPartyIDs.GroupCount();
				NoSettlPartyIDs = new AllocationInstructionNoSettlPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlPartyIDs[i] = new();
					((IFixParser)NoSettlPartyIDs[i]).Parse(viewNoSettlPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlPartyIDs":
					value = NoSettlPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSettlPartyIDs = null;
		}
	}
}
