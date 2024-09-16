using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BG", FixVersion.FIX44)]
	public sealed partial class CollateralInquiryAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 909, Type = TagType.String, Offset = 1, Required = true)]
		public string? CollInquiryID { get; set; }
		
		[TagDetails(Tag = 945, Type = TagType.Int, Offset = 2, Required = true)]
		public int? CollInquiryStatus { get; set; }
		
		[TagDetails(Tag = 946, Type = TagType.Int, Offset = 3, Required = false)]
		public int? CollInquiryResult { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public CollInqQualGrp? CollInqQualGrp { get; set; }
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TotNumReports { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 8, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 9, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 10, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 11, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 12, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component(Offset = 14, Required = false)]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component(Offset = 15, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 16, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 18, Required = false)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 19, Required = false)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 20, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 23, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 24, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 25, Required = false)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 26, Required = false)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 27, Required = false)]
		public DateOnly? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 28, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 29, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 30, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 31, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 32, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 33, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& CollInquiryID is not null
				&& CollInquiryStatus is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (CollInquiryID is not null) writer.WriteString(909, CollInquiryID);
			if (CollInquiryStatus is not null) writer.WriteWholeNumber(945, CollInquiryStatus.Value);
			if (CollInquiryResult is not null) writer.WriteWholeNumber(946, CollInquiryResult.Value);
			if (CollInqQualGrp is not null) ((IFixEncoder)CollInqQualGrp).Encode(writer);
			if (TotNumReports is not null) writer.WriteWholeNumber(911, TotNumReports.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ExecCollGrp is not null) ((IFixEncoder)ExecCollGrp).Encode(writer);
			if (TrdCollGrp is not null) ((IFixEncoder)TrdCollGrp).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
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
