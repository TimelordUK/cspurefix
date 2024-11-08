using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AllocRegulatoryTradeIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1908, Offset = 0, Required = false)]
		public AllocationInstructionNoAllocRegulatoryTradeIDs[]? NoAllocRegulatoryTradeIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAllocRegulatoryTradeIDs is not null && NoAllocRegulatoryTradeIDs.Length != 0)
			{
				writer.WriteWholeNumber(1908, NoAllocRegulatoryTradeIDs.Length);
				for (int i = 0; i < NoAllocRegulatoryTradeIDs.Length; i++)
				{
					((IFixEncoder)NoAllocRegulatoryTradeIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAllocRegulatoryTradeIDs") is IMessageView viewNoAllocRegulatoryTradeIDs)
			{
				var count = viewNoAllocRegulatoryTradeIDs.GroupCount();
				NoAllocRegulatoryTradeIDs = new AllocationInstructionNoAllocRegulatoryTradeIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoAllocRegulatoryTradeIDs[i] = new();
					((IFixParser)NoAllocRegulatoryTradeIDs[i]).Parse(viewNoAllocRegulatoryTradeIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAllocRegulatoryTradeIDs":
					value = NoAllocRegulatoryTradeIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAllocRegulatoryTradeIDs = null;
		}
	}
}
