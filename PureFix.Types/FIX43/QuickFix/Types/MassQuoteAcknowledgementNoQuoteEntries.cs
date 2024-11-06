using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class MassQuoteAcknowledgementNoQuoteEntries : IFixGroup
	{
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 0, Required = false)]
		public string? QuoteEntryID {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 2, Required = false)]
		public double? BidPx {get; set;}
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 3, Required = false)]
		public double? OfferPx {get; set;}
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 4, Required = false)]
		public double? BidSize {get; set;}
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 5, Required = false)]
		public double? OfferSize {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 6, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 7, Required = false)]
		public double? BidSpotRate {get; set;}
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 8, Required = false)]
		public double? OfferSpotRate {get; set;}
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 9, Required = false)]
		public double? BidForwardPoints {get; set;}
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 10, Required = false)]
		public double? OfferForwardPoints {get; set;}
		
		[TagDetails(Tag = 631, Type = TagType.Float, Offset = 11, Required = false)]
		public double? MidPx {get; set;}
		
		[TagDetails(Tag = 632, Type = TagType.Float, Offset = 12, Required = false)]
		public double? BidYield {get; set;}
		
		[TagDetails(Tag = 633, Type = TagType.Float, Offset = 13, Required = false)]
		public double? MidYield {get; set;}
		
		[TagDetails(Tag = 634, Type = TagType.Float, Offset = 14, Required = false)]
		public double? OfferYield {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 15, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 16, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 17, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 18, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 19, Required = false)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 20, Required = false)]
		public DateOnly? FutSettDate2 {get; set;}
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 21, Required = false)]
		public double? OrderQty2 {get; set;}
		
		[TagDetails(Tag = 642, Type = TagType.Float, Offset = 22, Required = false)]
		public double? BidForwardPoints2 {get; set;}
		
		[TagDetails(Tag = 643, Type = TagType.Float, Offset = 23, Required = false)]
		public double? OfferForwardPoints2 {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 24, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 368, Type = TagType.Int, Offset = 25, Required = false)]
		public int? QuoteEntryRejectReason {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteEntryID is not null) writer.WriteString(299, QuoteEntryID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (BidPx is not null) writer.WriteNumber(132, BidPx.Value);
			if (OfferPx is not null) writer.WriteNumber(133, OfferPx.Value);
			if (BidSize is not null) writer.WriteNumber(134, BidSize.Value);
			if (OfferSize is not null) writer.WriteNumber(135, OfferSize.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (BidSpotRate is not null) writer.WriteNumber(188, BidSpotRate.Value);
			if (OfferSpotRate is not null) writer.WriteNumber(190, OfferSpotRate.Value);
			if (BidForwardPoints is not null) writer.WriteNumber(189, BidForwardPoints.Value);
			if (OfferForwardPoints is not null) writer.WriteNumber(191, OfferForwardPoints.Value);
			if (MidPx is not null) writer.WriteNumber(631, MidPx.Value);
			if (BidYield is not null) writer.WriteNumber(632, BidYield.Value);
			if (MidYield is not null) writer.WriteNumber(633, MidYield.Value);
			if (OfferYield is not null) writer.WriteNumber(634, OfferYield.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (BidForwardPoints2 is not null) writer.WriteNumber(642, BidForwardPoints2.Value);
			if (OfferForwardPoints2 is not null) writer.WriteNumber(643, OfferForwardPoints2.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (QuoteEntryRejectReason is not null) writer.WriteWholeNumber(368, QuoteEntryRejectReason.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			QuoteEntryID = view.GetString(299);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			BidPx = view.GetDouble(132);
			OfferPx = view.GetDouble(133);
			BidSize = view.GetDouble(134);
			OfferSize = view.GetDouble(135);
			ValidUntilTime = view.GetDateTime(62);
			BidSpotRate = view.GetDouble(188);
			OfferSpotRate = view.GetDouble(190);
			BidForwardPoints = view.GetDouble(189);
			OfferForwardPoints = view.GetDouble(191);
			MidPx = view.GetDouble(631);
			BidYield = view.GetDouble(632);
			MidYield = view.GetDouble(633);
			OfferYield = view.GetDouble(634);
			TransactTime = view.GetDateTime(60);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			FutSettDate = view.GetDateOnly(64);
			OrdType = view.GetString(40);
			FutSettDate2 = view.GetDateOnly(193);
			OrderQty2 = view.GetDouble(192);
			BidForwardPoints2 = view.GetDouble(642);
			OfferForwardPoints2 = view.GetDouble(643);
			Currency = view.GetString(15);
			QuoteEntryRejectReason = view.GetInt32(368);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "QuoteEntryID":
					value = QuoteEntryID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "BidPx":
					value = BidPx;
					break;
				case "OfferPx":
					value = OfferPx;
					break;
				case "BidSize":
					value = BidSize;
					break;
				case "OfferSize":
					value = OfferSize;
					break;
				case "ValidUntilTime":
					value = ValidUntilTime;
					break;
				case "BidSpotRate":
					value = BidSpotRate;
					break;
				case "OfferSpotRate":
					value = OfferSpotRate;
					break;
				case "BidForwardPoints":
					value = BidForwardPoints;
					break;
				case "OfferForwardPoints":
					value = OfferForwardPoints;
					break;
				case "MidPx":
					value = MidPx;
					break;
				case "BidYield":
					value = BidYield;
					break;
				case "MidYield":
					value = MidYield;
					break;
				case "OfferYield":
					value = OfferYield;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "FutSettDate":
					value = FutSettDate;
					break;
				case "OrdType":
					value = OrdType;
					break;
				case "FutSettDate2":
					value = FutSettDate2;
					break;
				case "OrderQty2":
					value = OrderQty2;
					break;
				case "BidForwardPoints2":
					value = BidForwardPoints2;
					break;
				case "OfferForwardPoints2":
					value = OfferForwardPoints2;
					break;
				case "Currency":
					value = Currency;
					break;
				case "QuoteEntryRejectReason":
					value = QuoteEntryRejectReason;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			QuoteEntryID = null;
			((IFixReset?)Instrument)?.Reset();
			BidPx = null;
			OfferPx = null;
			BidSize = null;
			OfferSize = null;
			ValidUntilTime = null;
			BidSpotRate = null;
			OfferSpotRate = null;
			BidForwardPoints = null;
			OfferForwardPoints = null;
			MidPx = null;
			BidYield = null;
			MidYield = null;
			OfferYield = null;
			TransactTime = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			FutSettDate = null;
			OrdType = null;
			FutSettDate2 = null;
			OrderQty2 = null;
			BidForwardPoints2 = null;
			OfferForwardPoints2 = null;
			Currency = null;
			QuoteEntryRejectReason = null;
		}
	}
}
