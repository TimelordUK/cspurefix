using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("f", FixVersion.FIX44)]
	public sealed partial class SecurityStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecurityStatusReqID {get; set;}
		
		[Component(Offset = 2, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 7, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 9, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 326, Type = TagType.Int, Offset = 10, Required = false)]
		public int? SecurityTradingStatus {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 11, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 12, Required = false)]
		public string? CorporateAction {get; set;}
		
		[TagDetails(Tag = 327, Type = TagType.String, Offset = 13, Required = false)]
		public string? HaltReasonChar {get; set;}
		
		[TagDetails(Tag = 328, Type = TagType.Boolean, Offset = 14, Required = false)]
		public bool? InViewOfCommon {get; set;}
		
		[TagDetails(Tag = 329, Type = TagType.Boolean, Offset = 15, Required = false)]
		public bool? DueToRelated {get; set;}
		
		[TagDetails(Tag = 330, Type = TagType.Float, Offset = 16, Required = false)]
		public double? BuyVolume {get; set;}
		
		[TagDetails(Tag = 331, Type = TagType.Float, Offset = 17, Required = false)]
		public double? SellVolume {get; set;}
		
		[TagDetails(Tag = 332, Type = TagType.Float, Offset = 18, Required = false)]
		public double? HighPx {get; set;}
		
		[TagDetails(Tag = 333, Type = TagType.Float, Offset = 19, Required = false)]
		public double? LowPx {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 20, Required = false)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 21, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 334, Type = TagType.Int, Offset = 22, Required = false)]
		public int? Adjustment {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 23, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 24, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 25, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 26, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityStatusReqID is not null) writer.WriteString(324, SecurityStatusReqID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (SecurityTradingStatus is not null) writer.WriteWholeNumber(326, SecurityTradingStatus.Value);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (HaltReasonChar is not null) writer.WriteString(327, HaltReasonChar);
			if (InViewOfCommon is not null) writer.WriteBoolean(328, InViewOfCommon.Value);
			if (DueToRelated is not null) writer.WriteBoolean(329, DueToRelated.Value);
			if (BuyVolume is not null) writer.WriteNumber(330, BuyVolume.Value);
			if (SellVolume is not null) writer.WriteNumber(331, SellVolume.Value);
			if (HighPx is not null) writer.WriteNumber(332, HighPx.Value);
			if (LowPx is not null) writer.WriteNumber(333, LowPx.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Adjustment is not null) writer.WriteWholeNumber(334, Adjustment.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			SecurityStatusReqID = view.GetString(324);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("InstrumentExtension") is IMessageView viewInstrumentExtension)
			{
				InstrumentExtension = new();
				((IFixParser)InstrumentExtension).Parse(viewInstrumentExtension);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			Currency = view.GetString(15);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			UnsolicitedIndicator = view.GetBool(325);
			SecurityTradingStatus = view.GetInt32(326);
			FinancialStatus = view.GetString(291);
			CorporateAction = view.GetString(292);
			HaltReasonChar = view.GetString(327);
			InViewOfCommon = view.GetBool(328);
			DueToRelated = view.GetBool(329);
			BuyVolume = view.GetDouble(330);
			SellVolume = view.GetDouble(331);
			HighPx = view.GetDouble(332);
			LowPx = view.GetDouble(333);
			LastPx = view.GetDouble(31);
			TransactTime = view.GetDateTime(60);
			Adjustment = view.GetInt32(334);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "SecurityStatusReqID":
					value = SecurityStatusReqID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "InstrumentExtension":
					value = InstrumentExtension;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "Currency":
					value = Currency;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "UnsolicitedIndicator":
					value = UnsolicitedIndicator;
					break;
				case "SecurityTradingStatus":
					value = SecurityTradingStatus;
					break;
				case "FinancialStatus":
					value = FinancialStatus;
					break;
				case "CorporateAction":
					value = CorporateAction;
					break;
				case "HaltReasonChar":
					value = HaltReasonChar;
					break;
				case "InViewOfCommon":
					value = InViewOfCommon;
					break;
				case "DueToRelated":
					value = DueToRelated;
					break;
				case "BuyVolume":
					value = BuyVolume;
					break;
				case "SellVolume":
					value = SellVolume;
					break;
				case "HighPx":
					value = HighPx;
					break;
				case "LowPx":
					value = LowPx;
					break;
				case "LastPx":
					value = LastPx;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "Adjustment":
					value = Adjustment;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
