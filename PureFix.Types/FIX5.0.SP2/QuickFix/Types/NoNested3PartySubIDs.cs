using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoNested3PartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 953, Type = TagType.String, Offset = 0, Required = false)]
		public string? Nested3PartySubID {get; set;}
		
		[TagDetails(Tag = 954, Type = TagType.Int, Offset = 1, Required = false)]
		public int? Nested3PartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Nested3PartySubID is not null) writer.WriteString(953, Nested3PartySubID);
			if (Nested3PartySubIDType is not null) writer.WriteWholeNumber(954, Nested3PartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Nested3PartySubID = view.GetString(953);
			Nested3PartySubIDType = view.GetInt32(954);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Nested3PartySubID":
					value = Nested3PartySubID;
					break;
				case "Nested3PartySubIDType":
					value = Nested3PartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
