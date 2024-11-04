using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class NoBidComponents : IFixGroup
	{
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 0, Required = false)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 1, Required = false)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 430, Type = TagType.Int, Offset = 3, Required = false)]
		public int? NetGrossInd {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 4, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ListID is not null) writer.WriteString(66, ListID);
			if (Side is not null) writer.WriteString(54, Side);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (NetGrossInd is not null) writer.WriteWholeNumber(430, NetGrossInd.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (Account is not null) writer.WriteString(1, Account);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ListID = view.GetString(66);
			Side = view.GetString(54);
			TradingSessionID = view.GetString(336);
			NetGrossInd = view.GetInt32(430);
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			Account = view.GetString(1);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ListID":
					value = ListID;
					break;
				case "Side":
					value = Side;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "NetGrossInd":
					value = NetGrossInd;
					break;
				case "SettlmntTyp":
					value = SettlmntTyp;
					break;
				case "FutSettDate":
					value = FutSettDate;
					break;
				case "Account":
					value = Account;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ListID = null;
			Side = null;
			TradingSessionID = null;
			NetGrossInd = null;
			SettlmntTyp = null;
			FutSettDate = null;
			Account = null;
		}
	}
}
