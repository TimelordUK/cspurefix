using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class SecListGrpNoRelatedSym
	{
		[Component(Offset = 0, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 4, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public InstrmtLegSecListGrp? InstrmtLegSecListGrp { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 9, Required = false)]
		public double? RoundLot { get; set; }
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 10, Required = false)]
		public double? MinTradeVol { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 13, Required = false)]
		public int? ExpirationCycle { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
	}
}
