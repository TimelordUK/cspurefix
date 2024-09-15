using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotEntryGrpNoQuoteEntries : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 0, Required = true)]
		public string? QuoteEntryID { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 3, Required = false)]
		public double? BidPx { get; set; }
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 4, Required = false)]
		public double? OfferPx { get; set; }
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 5, Required = false)]
		public double? BidSize { get; set; }
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 6, Required = false)]
		public double? OfferSize { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 7, Required = false)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 8, Required = false)]
		public double? BidSpotRate { get; set; }
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 9, Required = false)]
		public double? OfferSpotRate { get; set; }
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 10, Required = false)]
		public double? BidForwardPoints { get; set; }
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 11, Required = false)]
		public double? OfferForwardPoints { get; set; }
		
		[TagDetails(Tag = 631, Type = TagType.Float, Offset = 12, Required = false)]
		public double? MidPx { get; set; }
		
		[TagDetails(Tag = 632, Type = TagType.Float, Offset = 13, Required = false)]
		public double? BidYield { get; set; }
		
		[TagDetails(Tag = 633, Type = TagType.Float, Offset = 14, Required = false)]
		public double? MidYield { get; set; }
		
		[TagDetails(Tag = 634, Type = TagType.Float, Offset = 15, Required = false)]
		public double? OfferYield { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 16, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 17, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 18, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 19, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 20, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 21, Required = false)]
		public DateOnly? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 22, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 642, Type = TagType.Float, Offset = 23, Required = false)]
		public double? BidForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 643, Type = TagType.Float, Offset = 24, Required = false)]
		public double? OfferForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 25, Required = false)]
		public string? Currency { get; set; }
		
	}
}
