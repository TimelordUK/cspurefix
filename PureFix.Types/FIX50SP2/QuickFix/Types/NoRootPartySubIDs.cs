using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoRootPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 1121, Type = TagType.String, Offset = 0, Required = false)]
		public string? RootPartySubID {get; set;}
		
		[TagDetails(Tag = 1122, Type = TagType.Int, Offset = 1, Required = false)]
		public int? RootPartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RootPartySubID is not null) writer.WriteString(1121, RootPartySubID);
			if (RootPartySubIDType is not null) writer.WriteWholeNumber(1122, RootPartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RootPartySubID = view.GetString(1121);
			RootPartySubIDType = view.GetInt32(1122);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RootPartySubID":
					value = RootPartySubID;
					break;
				case "RootPartySubIDType":
					value = RootPartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RootPartySubID = null;
			RootPartySubIDType = null;
		}
	}
}
