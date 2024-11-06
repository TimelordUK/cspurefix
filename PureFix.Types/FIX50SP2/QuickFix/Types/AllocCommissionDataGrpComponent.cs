using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AllocCommissionDataGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2653, Offset = 0, Required = false)]
		public AllocationInstructionNoAllocCommissions[]? NoAllocCommissions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAllocCommissions is not null && NoAllocCommissions.Length != 0)
			{
				writer.WriteWholeNumber(2653, NoAllocCommissions.Length);
				for (int i = 0; i < NoAllocCommissions.Length; i++)
				{
					((IFixEncoder)NoAllocCommissions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAllocCommissions") is IMessageView viewNoAllocCommissions)
			{
				var count = viewNoAllocCommissions.GroupCount();
				NoAllocCommissions = new AllocationInstructionNoAllocCommissions[count];
				for (int i = 0; i < count; i++)
				{
					NoAllocCommissions[i] = new();
					((IFixParser)NoAllocCommissions[i]).Parse(viewNoAllocCommissions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAllocCommissions":
					value = NoAllocCommissions;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAllocCommissions = null;
		}
	}
}
