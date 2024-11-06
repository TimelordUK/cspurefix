using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecutionReportNoNested4PartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 1412, Type = TagType.String, Offset = 0, Required = false)]
		public string? Nested4PartySubID {get; set;}
		
		[TagDetails(Tag = 1411, Type = TagType.Int, Offset = 1, Required = false)]
		public int? Nested4PartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Nested4PartySubID is not null) writer.WriteString(1412, Nested4PartySubID);
			if (Nested4PartySubIDType is not null) writer.WriteWholeNumber(1411, Nested4PartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Nested4PartySubID = view.GetString(1412);
			Nested4PartySubIDType = view.GetInt32(1411);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Nested4PartySubID":
					value = Nested4PartySubID;
					break;
				case "Nested4PartySubIDType":
					value = Nested4PartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Nested4PartySubID = null;
			Nested4PartySubIDType = null;
		}
	}
}
