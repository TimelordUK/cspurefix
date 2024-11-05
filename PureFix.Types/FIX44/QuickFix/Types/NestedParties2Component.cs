using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedParties2Component : IFixComponent
	{
		[Group(NoOfTag = 756, Offset = 0, Required = false)]
		public AllocationInstructionOrdAllocGrpNoOrdersNestedParties2NoNested2PartyIDs[]? NoNested2PartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested2PartyIDs is not null && NoNested2PartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(756, NoNested2PartyIDs.Length);
				for (int i = 0; i < NoNested2PartyIDs.Length; i++)
				{
					((IFixEncoder)NoNested2PartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNested2PartyIDs") is IMessageView viewNoNested2PartyIDs)
			{
				var count = viewNoNested2PartyIDs.GroupCount();
				NoNested2PartyIDs = new AllocationInstructionOrdAllocGrpNoOrdersNestedParties2NoNested2PartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNested2PartyIDs[i] = new();
					((IFixParser)NoNested2PartyIDs[i]).Parse(viewNoNested2PartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNested2PartyIDs":
					value = NoNested2PartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNested2PartyIDs = null;
		}
	}
}
