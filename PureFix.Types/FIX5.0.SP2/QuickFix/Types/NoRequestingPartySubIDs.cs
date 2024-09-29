using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoRequestingPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 1662, Type = TagType.String, Offset = 0, Required = false)]
		public string? RequestingPartySubID {get; set;}
		
		[TagDetails(Tag = 1663, Type = TagType.Int, Offset = 1, Required = false)]
		public int? RequestingPartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RequestingPartySubID is not null) writer.WriteString(1662, RequestingPartySubID);
			if (RequestingPartySubIDType is not null) writer.WriteWholeNumber(1663, RequestingPartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RequestingPartySubID = view.GetString(1662);
			RequestingPartySubIDType = view.GetInt32(1663);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RequestingPartySubID":
					value = RequestingPartySubID;
					break;
				case "RequestingPartySubIDType":
					value = RequestingPartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RequestingPartySubID = null;
			RequestingPartySubIDType = null;
		}
	}
}
