using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ExecutionReportPartiesNoPartyIDsPtysSubGrpNoPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 523, Type = TagType.String, Offset = 0, Required = false)]
		public string? PartySubID {get; set;}
		
		[TagDetails(Tag = 803, Type = TagType.Int, Offset = 1, Required = false)]
		public int? PartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PartySubID is not null) writer.WriteString(523, PartySubID);
			if (PartySubIDType is not null) writer.WriteWholeNumber(803, PartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PartySubID = view.GetString(523);
			PartySubIDType = view.GetInt32(803);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PartySubID":
					value = PartySubID;
					break;
				case "PartySubIDType":
					value = PartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PartySubID = null;
			PartySubIDType = null;
		}
	}
}
