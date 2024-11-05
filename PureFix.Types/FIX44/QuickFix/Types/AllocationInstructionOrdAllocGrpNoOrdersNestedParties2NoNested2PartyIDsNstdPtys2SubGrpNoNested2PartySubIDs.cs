using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AllocationInstructionOrdAllocGrpNoOrdersNestedParties2NoNested2PartyIDsNstdPtys2SubGrpNoNested2PartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 760, Type = TagType.String, Offset = 0, Required = false)]
		public string? Nested2PartySubID {get; set;}
		
		[TagDetails(Tag = 807, Type = TagType.Int, Offset = 1, Required = false)]
		public int? Nested2PartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Nested2PartySubID is not null) writer.WriteString(760, Nested2PartySubID);
			if (Nested2PartySubIDType is not null) writer.WriteWholeNumber(807, Nested2PartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Nested2PartySubID = view.GetString(760);
			Nested2PartySubIDType = view.GetInt32(807);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Nested2PartySubID":
					value = Nested2PartySubID;
					break;
				case "Nested2PartySubIDType":
					value = Nested2PartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Nested2PartySubID = null;
			Nested2PartySubIDType = null;
		}
	}
}
