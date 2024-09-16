using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class BidResponseNoBidComponents : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = true)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 421, Type = TagType.String, Offset = 2, Required = false)]
		public string? Country { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 3, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 4, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 5, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 406, Type = TagType.Float, Offset = 6, Required = false)]
		public double? FairValue { get; set; }
		
		[TagDetails(Tag = 430, Type = TagType.Int, Offset = 7, Required = false)]
		public int? NetGrossInd { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 8, Required = false)]
		public string? SettlmntTyp { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 9, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 12, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 13, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 14, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				CommissionData is not null && ((IFixValidator)CommissionData).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (Country is not null) writer.WriteString(421, Country);
			if (Side is not null) writer.WriteString(54, Side);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (FairValue is not null) writer.WriteNumber(406, FairValue.Value);
			if (NetGrossInd is not null) writer.WriteWholeNumber(430, NetGrossInd.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
		}
	}
}
