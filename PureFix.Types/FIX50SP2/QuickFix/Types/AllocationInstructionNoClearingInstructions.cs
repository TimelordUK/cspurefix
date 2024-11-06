using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AllocationInstructionNoClearingInstructions : IFixGroup
	{
		[TagDetails(Tag = 577, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ClearingInstruction {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClearingInstruction is not null) writer.WriteWholeNumber(577, ClearingInstruction.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ClearingInstruction = view.GetInt32(577);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ClearingInstruction":
					value = ClearingInstruction;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ClearingInstruction = null;
		}
	}
}
