using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class BidRequestNoBidComponents : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 0, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 1, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 430, Type = TagType.Int, Offset = 3, Required = false)]
		public int? NetGrossInd { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 4, Required = false)]
		public string? SettlmntTyp { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account { get; set; }
		
		
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
	}
}
