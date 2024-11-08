using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NstdPtys2SubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 806, Offset = 0, Required = false)]
		public AllocationInstructionNoNested2PartySubIDs[]? NoNested2PartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested2PartySubIDs is not null && NoNested2PartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(806, NoNested2PartySubIDs.Length);
				for (int i = 0; i < NoNested2PartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNested2PartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNested2PartySubIDs") is IMessageView viewNoNested2PartySubIDs)
			{
				var count = viewNoNested2PartySubIDs.GroupCount();
				NoNested2PartySubIDs = new AllocationInstructionNoNested2PartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNested2PartySubIDs[i] = new();
					((IFixParser)NoNested2PartySubIDs[i]).Parse(viewNoNested2PartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNested2PartySubIDs":
					value = NoNested2PartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNested2PartySubIDs = null;
		}
	}
}
