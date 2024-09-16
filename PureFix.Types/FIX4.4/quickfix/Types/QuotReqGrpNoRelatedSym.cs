using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotReqGrpNoRelatedSym : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 3, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 303, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRequestType { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteType { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 7, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 8, Required = false)]
		public DateOnly? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 9, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 10, Required = false)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 12, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 14, Required = false)]
		public DateOnly? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 15, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 16, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 18, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 19, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 20, Required = false)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public QuotReqLegsGrp? QuotReqLegsGrp { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public QuotQualGrp? QuotQualGrp { get; set; }
		
		[TagDetails(Tag = 692, Type = TagType.Int, Offset = 23, Required = false)]
		public int? QuotePriceType { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 24, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 25, Required = false)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 26, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 27, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 28, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 29, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 30, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 31, Required = false)]
		public double? Price2 { get; set; }
		
		[Component(Offset = 32, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 33, Required = false)]
		public Parties? Parties { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Instrument is not null && ((IFixValidator)Instrument).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (QuoteRequestType is not null) writer.WriteWholeNumber(303, QuoteRequestType.Value);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (SettlDate2 is not null) writer.WriteLocalDateOnly(193, SettlDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (QuotReqLegsGrp is not null) ((IFixEncoder)QuotReqLegsGrp).Encode(writer);
			if (QuotQualGrp is not null) ((IFixEncoder)QuotQualGrp).Encode(writer);
			if (QuotePriceType is not null) writer.WriteWholeNumber(692, QuotePriceType.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (Price2 is not null) writer.WriteNumber(640, Price2.Value);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
		}
	}
}
