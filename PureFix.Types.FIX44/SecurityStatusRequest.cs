using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("e", FixVersion.FIX44)]
	public sealed partial class SecurityStatusRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityStatusReqID {get; set;}
		
		[Component(Offset = 2, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public InstrumentExtension? InstrumentExtension {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 7, Required = true)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
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
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
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
			SubscriptionRequestType = view.GetString(263);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
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
				{
					value = StandardHeader;
					break;
				}
				case "SecurityStatusReqID":
				{
					value = SecurityStatusReqID;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "InstrumentExtension":
				{
					value = InstrumentExtension;
					break;
				}
				case "UndInstrmtGrp":
				{
					value = UndInstrmtGrp;
					break;
				}
				case "InstrmtLegGrp":
				{
					value = InstrmtLegGrp;
					break;
				}
				case "Currency":
				{
					value = Currency;
					break;
				}
				case "SubscriptionRequestType":
				{
					value = SubscriptionRequestType;
					break;
				}
				case "TradingSessionID":
				{
					value = TradingSessionID;
					break;
				}
				case "TradingSessionSubID":
				{
					value = TradingSessionSubID;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			SecurityStatusReqID = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)InstrumentExtension)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			((IFixReset?)InstrmtLegGrp)?.Reset();
			Currency = null;
			SubscriptionRequestType = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
