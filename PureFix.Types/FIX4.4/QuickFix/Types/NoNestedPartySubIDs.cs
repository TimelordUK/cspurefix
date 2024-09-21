using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NoNestedPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 545, Type = TagType.String, Offset = 0, Required = false)]
		public string? NestedPartySubID {get; set;}
		
		[TagDetails(Tag = 805, Type = TagType.Int, Offset = 1, Required = false)]
		public int? NestedPartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NestedPartySubID is not null) writer.WriteString(545, NestedPartySubID);
			if (NestedPartySubIDType is not null) writer.WriteWholeNumber(805, NestedPartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			NestedPartySubID = view.GetString(545);
			NestedPartySubIDType = view.GetInt32(805);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NestedPartySubID":
					value = NestedPartySubID;
					break;
				case "NestedPartySubIDType":
					value = NestedPartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NestedPartySubID = null;
			NestedPartySubIDType = null;
		}
	}
}
