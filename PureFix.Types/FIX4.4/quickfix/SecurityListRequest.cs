using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("x", FixVersion.FIX44)]
	public sealed partial class SecurityListRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 559, Type = TagType.Int, Offset = 2, Required = true)]
		public int? SecurityListRequestType {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 9, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 10, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 11, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 13, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[Component(Offset = 15, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SecurityReqID is not null
				&& SecurityListRequestType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityListRequestType is not null) writer.WriteWholeNumber(559, SecurityListRequestType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
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
			SecurityReqID = view.GetString(320);
			SecurityListRequestType = view.GetInt32(559);
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
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
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
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			SubscriptionRequestType = view.GetString(263);
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
				case "SecurityReqID":
					value = SecurityReqID;
					break;
				case "SecurityListRequestType":
					value = SecurityListRequestType;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "InstrumentExtension":
					value = InstrumentExtension;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
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
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
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
