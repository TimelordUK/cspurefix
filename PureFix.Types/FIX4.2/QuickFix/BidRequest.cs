using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("k", FixVersion.FIX42)]
	public sealed partial class BidRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1, Required = false)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2, Required = true)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(Tag = 374, Type = TagType.String, Offset = 3, Required = true)]
		public string? BidRequestTransType { get; set; }
		
		[TagDetails(Tag = 392, Type = TagType.String, Offset = 4, Required = false)]
		public string? ListName { get; set; }
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 5, Required = true)]
		public int? TotalNumSecurities { get; set; }
		
		[TagDetails(Tag = 394, Type = TagType.Int, Offset = 6, Required = true)]
		public int? BidType { get; set; }
		
		[TagDetails(Tag = 395, Type = TagType.Int, Offset = 7, Required = false)]
		public int? NumTickets { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 396, Type = TagType.Float, Offset = 9, Required = false)]
		public double? SideValue1 { get; set; }
		
		[TagDetails(Tag = 397, Type = TagType.Float, Offset = 10, Required = false)]
		public double? SideValue2 { get; set; }
		
		[Group(NoOfTag = 398, Offset = 11, Required = false)]
		public BidRequestNoBidDescriptors[]? NoBidDescriptors { get; set; }
		
		[Group(NoOfTag = 420, Offset = 12, Required = false)]
		public BidRequestNoBidComponents[]? NoBidComponents { get; set; }
		
		[TagDetails(Tag = 409, Type = TagType.Int, Offset = 13, Required = false)]
		public int? LiquidityIndType { get; set; }
		
		[TagDetails(Tag = 410, Type = TagType.Float, Offset = 14, Required = false)]
		public double? WtAverageLiquidity { get; set; }
		
		[TagDetails(Tag = 411, Type = TagType.Boolean, Offset = 15, Required = false)]
		public bool? ExchangeForPhysical { get; set; }
		
		[TagDetails(Tag = 412, Type = TagType.Float, Offset = 16, Required = false)]
		public double? OutMainCntryUIndex { get; set; }
		
		[TagDetails(Tag = 413, Type = TagType.Float, Offset = 17, Required = false)]
		public double? CrossPercent { get; set; }
		
		[TagDetails(Tag = 414, Type = TagType.Int, Offset = 18, Required = false)]
		public int? ProgRptReqs { get; set; }
		
		[TagDetails(Tag = 415, Type = TagType.Int, Offset = 19, Required = false)]
		public int? ProgPeriodInterval { get; set; }
		
		[TagDetails(Tag = 416, Type = TagType.Int, Offset = 20, Required = false)]
		public int? IncTaxInd { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 21, Required = false)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 417, Type = TagType.Int, Offset = 22, Required = false)]
		public int? NumBidders { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 23, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 418, Type = TagType.String, Offset = 24, Required = true)]
		public string? TradeType { get; set; }
		
		[TagDetails(Tag = 419, Type = TagType.String, Offset = 25, Required = true)]
		public string? BasisPxType { get; set; }
		
		[TagDetails(Tag = 443, Type = TagType.UtcTimestamp, Offset = 26, Required = false)]
		public DateTime? StrikeTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 27, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 28, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 29, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 30, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ClientBidID is not null
				&& BidRequestTransType is not null
				&& TotalNumSecurities is not null
				&& BidType is not null
				&& TradeType is not null
				&& BasisPxType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (BidID is not null) writer.WriteString(390, BidID);
			if (ClientBidID is not null) writer.WriteString(391, ClientBidID);
			if (BidRequestTransType is not null) writer.WriteString(374, BidRequestTransType);
			if (ListName is not null) writer.WriteString(392, ListName);
			if (TotalNumSecurities is not null) writer.WriteWholeNumber(393, TotalNumSecurities.Value);
			if (BidType is not null) writer.WriteWholeNumber(394, BidType.Value);
			if (NumTickets is not null) writer.WriteWholeNumber(395, NumTickets.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (SideValue1 is not null) writer.WriteNumber(396, SideValue1.Value);
			if (SideValue2 is not null) writer.WriteNumber(397, SideValue2.Value);
			if (NoBidDescriptors is not null && NoBidDescriptors.Length != 0)
			{
				writer.WriteWholeNumber(398, NoBidDescriptors.Length);
				for (int i = 0; i < NoBidDescriptors.Length; i++)
				{
					((IFixEncoder)NoBidDescriptors[i]).Encode(writer);
				}
			}
			if (NoBidComponents is not null && NoBidComponents.Length != 0)
			{
				writer.WriteWholeNumber(420, NoBidComponents.Length);
				for (int i = 0; i < NoBidComponents.Length; i++)
				{
					((IFixEncoder)NoBidComponents[i]).Encode(writer);
				}
			}
			if (LiquidityIndType is not null) writer.WriteWholeNumber(409, LiquidityIndType.Value);
			if (WtAverageLiquidity is not null) writer.WriteNumber(410, WtAverageLiquidity.Value);
			if (ExchangeForPhysical is not null) writer.WriteBoolean(411, ExchangeForPhysical.Value);
			if (OutMainCntryUIndex is not null) writer.WriteNumber(412, OutMainCntryUIndex.Value);
			if (CrossPercent is not null) writer.WriteNumber(413, CrossPercent.Value);
			if (ProgRptReqs is not null) writer.WriteWholeNumber(414, ProgRptReqs.Value);
			if (ProgPeriodInterval is not null) writer.WriteWholeNumber(415, ProgPeriodInterval.Value);
			if (IncTaxInd is not null) writer.WriteWholeNumber(416, IncTaxInd.Value);
			if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
			if (NumBidders is not null) writer.WriteWholeNumber(417, NumBidders.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TradeType is not null) writer.WriteString(418, TradeType);
			if (BasisPxType is not null) writer.WriteString(419, BasisPxType);
			if (StrikeTime is not null) writer.WriteUtcTimeStamp(443, StrikeTime.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
