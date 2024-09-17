using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NoSettlPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 785, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlPartySubID {get; set;}
		
		[TagDetails(Tag = 786, Type = TagType.Int, Offset = 1, Required = false)]
		public int? SettlPartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SettlPartySubID is not null) writer.WriteString(785, SettlPartySubID);
			if (SettlPartySubIDType is not null) writer.WriteWholeNumber(786, SettlPartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SettlPartySubID = view.GetString(785);
			SettlPartySubIDType = view.GetInt32(786);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SettlPartySubID":
					value = SettlPartySubID;
					break;
				case "SettlPartySubIDType":
					value = SettlPartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
