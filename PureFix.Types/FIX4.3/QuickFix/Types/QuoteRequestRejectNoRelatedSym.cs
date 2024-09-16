using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class QuoteRequestRejectNoRelatedSym : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 1, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 303, Type = TagType.Int, Offset = 2, Required = false)]
		public int? QuoteRequestType { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 3, Required = false)]
		public int? QuoteType { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradeOriginationDate { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 8, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 465, Type = TagType.Int, Offset = 9, Required = false)]
		public int? QuantityType { get; set; }
		
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 10, Required = false)]
		public double? OrderQty { get; set; }
		
		[TagDetails(Tag = 152, Type = TagType.Float, Offset = 11, Required = false)]
		public double? CashOrderQty { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 12, Required = false)]
		public string? SettlmntTyp { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 14, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 15, Required = false)]
		public DateOnly? FutSettDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 16, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 17, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 19, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 20, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 21, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 22, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 23, Required = false)]
		public double? Price2 { get; set; }
		
		[Component(Offset = 24, Required = false)]
		public YieldData? YieldData { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Instrument is not null && ((IFixValidator)Instrument).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (QuoteRequestType is not null) writer.WriteWholeNumber(303, QuoteRequestType.Value);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TradeOriginationDate is not null) writer.WriteString(229, TradeOriginationDate);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
			if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
			if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (Price2 is not null) writer.WriteNumber(640, Price2.Value);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
		}
	}
}
