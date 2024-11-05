using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AttrbGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 870, Offset = 0, Required = false)]
		public AllocationInstructionInstrumentExtensionAttrbGrpNoInstrAttrib[]? NoInstrAttrib {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrAttrib is not null && NoInstrAttrib.Length != 0)
			{
				writer.WriteWholeNumber(870, NoInstrAttrib.Length);
				for (int i = 0; i < NoInstrAttrib.Length; i++)
				{
					((IFixEncoder)NoInstrAttrib[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoInstrAttrib") is IMessageView viewNoInstrAttrib)
			{
				var count = viewNoInstrAttrib.GroupCount();
				NoInstrAttrib = new AllocationInstructionInstrumentExtensionAttrbGrpNoInstrAttrib[count];
				for (int i = 0; i < count; i++)
				{
					NoInstrAttrib[i] = new();
					((IFixParser)NoInstrAttrib[i]).Parse(viewNoInstrAttrib.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoInstrAttrib":
					value = NoInstrAttrib;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoInstrAttrib = null;
		}
	}
}
