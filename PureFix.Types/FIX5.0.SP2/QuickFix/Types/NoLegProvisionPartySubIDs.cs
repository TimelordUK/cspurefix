using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegProvisionPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 40538, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProvisionPartySubID {get; set;}
		
		[TagDetails(Tag = 40539, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegProvisionPartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionPartySubID is not null) writer.WriteString(40538, LegProvisionPartySubID);
			if (LegProvisionPartySubIDType is not null) writer.WriteWholeNumber(40539, LegProvisionPartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionPartySubID = view.GetString(40538);
			LegProvisionPartySubIDType = view.GetInt32(40539);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionPartySubID":
					value = LegProvisionPartySubID;
					break;
				case "LegProvisionPartySubIDType":
					value = LegProvisionPartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
