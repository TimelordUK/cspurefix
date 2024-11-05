using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AllocGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 78, Offset = 0, Required = false)]
		public AllocationInstructionAllocGrpNoAllocs[]? NoAllocs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAllocs is not null && NoAllocs.Length != 0)
			{
				writer.WriteWholeNumber(78, NoAllocs.Length);
				for (int i = 0; i < NoAllocs.Length; i++)
				{
					((IFixEncoder)NoAllocs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAllocs") is IMessageView viewNoAllocs)
			{
				var count = viewNoAllocs.GroupCount();
				NoAllocs = new AllocationInstructionAllocGrpNoAllocs[count];
				for (int i = 0; i < count; i++)
				{
					NoAllocs[i] = new();
					((IFixParser)NoAllocs[i]).Parse(viewNoAllocs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAllocs":
					value = NoAllocs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAllocs = null;
		}
	}
}
