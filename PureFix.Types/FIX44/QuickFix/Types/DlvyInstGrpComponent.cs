using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class DlvyInstGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 85, Offset = 0, Required = false)]
		public AllocationInstructionAllocGrpNoAllocsSettlInstructionsDataDlvyInstGrpNoDlvyInst[]? NoDlvyInst {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDlvyInst is not null && NoDlvyInst.Length != 0)
			{
				writer.WriteWholeNumber(85, NoDlvyInst.Length);
				for (int i = 0; i < NoDlvyInst.Length; i++)
				{
					((IFixEncoder)NoDlvyInst[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDlvyInst") is IMessageView viewNoDlvyInst)
			{
				var count = viewNoDlvyInst.GroupCount();
				NoDlvyInst = new AllocationInstructionAllocGrpNoAllocsSettlInstructionsDataDlvyInstGrpNoDlvyInst[count];
				for (int i = 0; i < count; i++)
				{
					NoDlvyInst[i] = new();
					((IFixParser)NoDlvyInst[i]).Parse(viewNoDlvyInst.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDlvyInst":
					value = NoDlvyInst;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDlvyInst = null;
		}
	}
}
