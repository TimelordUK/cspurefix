using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotReqRjctGrpNoRelatedSym : IFixValidator, IFixEncoder
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
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 25, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 26, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 28, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 29, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 30, Required = false)]
		public double? Price2 { get; set; }
		
		[Component(Offset = 31, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 32, Required = false)]
		public Parties? Parties { get; set; }
		
	}
}
