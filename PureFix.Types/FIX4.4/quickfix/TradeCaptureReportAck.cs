using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AR", FixVersion.FIX44)]
	public sealed partial class TradeCaptureReportAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 6, Required = false)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 7, Required = false)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 8, Required = true)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 10, Required = false)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(Tag = 939, Type = TagType.Int, Offset = 11, Required = false)]
		public int? TrdRptStatus { get; set; }
		
		[TagDetails(Tag = 751, Type = TagType.Int, Offset = 12, Required = false)]
		public int? TradeReportRejectReason { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16, Required = false)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 17, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[Component(Offset = 19, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 20, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 22, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 23, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 28, Required = false)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 29, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 30, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 31, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 32, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 33, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 34, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 35, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 36, Required = false)]
		public string? PreallocMethod { get; set; }
		
		[Component(Offset = 37, Required = false)]
		public TrdAllocGrp? TrdAllocGrp { get; set; }
		
		[Component(Offset = 38, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeReportID is not null
				&& ExecType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (TradeReportTransType is not null) writer.WriteWholeNumber(487, TradeReportTransType.Value);
			if (TradeReportType is not null) writer.WriteWholeNumber(856, TradeReportType.Value);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (SecondaryTrdType is not null) writer.WriteWholeNumber(855, SecondaryTrdType.Value);
			if (TransferReason is not null) writer.WriteString(830, TransferReason);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (TradeReportRefID is not null) writer.WriteString(572, TradeReportRefID);
			if (SecondaryTradeReportRefID is not null) writer.WriteString(881, SecondaryTradeReportRefID);
			if (TrdRptStatus is not null) writer.WriteWholeNumber(939, TrdRptStatus.Value);
			if (TradeReportRejectReason is not null) writer.WriteWholeNumber(751, TradeReportRejectReason.Value);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TradeLinkID is not null) writer.WriteString(820, TradeLinkID);
			if (TrdMatchID is not null) writer.WriteString(880, TrdMatchID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TrdRegTimestamps is not null) ((IFixEncoder)TrdRegTimestamps).Encode(writer);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TrdInstrmtLegGrp is not null) ((IFixEncoder)TrdInstrmtLegGrp).Encode(writer);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (TrdAllocGrp is not null) ((IFixEncoder)TrdAllocGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
