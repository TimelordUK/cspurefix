using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DisclosureInstructionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1812, Offset = 0, Required = false)]
		public NoDisclosureInstructions[]? NoDisclosureInstructions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDisclosureInstructions is not null && NoDisclosureInstructions.Length != 0)
			{
				writer.WriteWholeNumber(1812, NoDisclosureInstructions.Length);
				for (int i = 0; i < NoDisclosureInstructions.Length; i++)
				{
					((IFixEncoder)NoDisclosureInstructions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDisclosureInstructions") is IMessageView viewNoDisclosureInstructions)
			{
				var count = viewNoDisclosureInstructions.GroupCount();
				NoDisclosureInstructions = new NoDisclosureInstructions[count];
				for (int i = 0; i < count; i++)
				{
					NoDisclosureInstructions[i] = new();
					((IFixParser)NoDisclosureInstructions[i]).Parse(viewNoDisclosureInstructions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDisclosureInstructions":
					value = NoDisclosureInstructions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
