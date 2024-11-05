using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ClrInstGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 576, Offset = 0, Required = false)]
		public AllocationInstructionAllocGrpNoAllocsClrInstGrpNoClearingInstructions[]? NoClearingInstructions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoClearingInstructions is not null && NoClearingInstructions.Length != 0)
			{
				writer.WriteWholeNumber(576, NoClearingInstructions.Length);
				for (int i = 0; i < NoClearingInstructions.Length; i++)
				{
					((IFixEncoder)NoClearingInstructions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoClearingInstructions") is IMessageView viewNoClearingInstructions)
			{
				var count = viewNoClearingInstructions.GroupCount();
				NoClearingInstructions = new AllocationInstructionAllocGrpNoAllocsClrInstGrpNoClearingInstructions[count];
				for (int i = 0; i < count; i++)
				{
					NoClearingInstructions[i] = new();
					((IFixParser)NoClearingInstructions[i]).Parse(viewNoClearingInstructions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoClearingInstructions":
					value = NoClearingInstructions;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoClearingInstructions = null;
		}
	}
}
