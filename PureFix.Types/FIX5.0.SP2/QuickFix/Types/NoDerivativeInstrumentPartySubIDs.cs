using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoDerivativeInstrumentPartySubIDs : IFixGroup
	{
		[TagDetails(Tag = 1297, Type = TagType.String, Offset = 0, Required = false)]
		public string? DerivativeInstrumentPartySubID {get; set;}
		
		[TagDetails(Tag = 1298, Type = TagType.Int, Offset = 1, Required = false)]
		public int? DerivativeInstrumentPartySubIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DerivativeInstrumentPartySubID is not null) writer.WriteString(1297, DerivativeInstrumentPartySubID);
			if (DerivativeInstrumentPartySubIDType is not null) writer.WriteWholeNumber(1298, DerivativeInstrumentPartySubIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DerivativeInstrumentPartySubID = view.GetString(1297);
			DerivativeInstrumentPartySubIDType = view.GetInt32(1298);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DerivativeInstrumentPartySubID":
					value = DerivativeInstrumentPartySubID;
					break;
				case "DerivativeInstrumentPartySubIDType":
					value = DerivativeInstrumentPartySubIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DerivativeInstrumentPartySubID = null;
			DerivativeInstrumentPartySubIDType = null;
		}
	}
}
