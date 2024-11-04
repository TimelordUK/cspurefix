using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoDisclosureInstructions : IFixGroup
	{
		[TagDetails(Tag = 1813, Type = TagType.Int, Offset = 0, Required = false)]
		public int? DisclosureType {get; set;}
		
		[TagDetails(Tag = 1814, Type = TagType.Int, Offset = 1, Required = false)]
		public int? DisclosureInstruction {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DisclosureType is not null) writer.WriteWholeNumber(1813, DisclosureType.Value);
			if (DisclosureInstruction is not null) writer.WriteWholeNumber(1814, DisclosureInstruction.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DisclosureType = view.GetInt32(1813);
			DisclosureInstruction = view.GetInt32(1814);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DisclosureType":
					value = DisclosureType;
					break;
				case "DisclosureInstruction":
					value = DisclosureInstruction;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DisclosureType = null;
			DisclosureInstruction = null;
		}
	}
}
