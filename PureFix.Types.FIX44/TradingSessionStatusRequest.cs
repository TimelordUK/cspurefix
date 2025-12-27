using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("g", FixVersion.FIX44)]
	public sealed partial class TradingSessionStatusRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 335, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradSesReqID {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 3, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 338, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TradSesMethod {get; set;}
		
		[TagDetails(Tag = 339, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TradSesMode {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 6, Required = true)]
		public string? SubscriptionRequestType {get; set;}
		
		[Component(Offset = 7, Required = true)]
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
			if (TradSesReqID is not null) writer.WriteString(335, TradSesReqID);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TradSesMethod is not null) writer.WriteWholeNumber(338, TradSesMethod.Value);
			if (TradSesMode is not null) writer.WriteWholeNumber(339, TradSesMode.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TradSesReqID = view.GetString(335);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			TradSesMethod = view.GetInt32(338);
			TradSesMode = view.GetInt32(339);
			SubscriptionRequestType = view.GetString(263);
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
				case "TradSesReqID":
				{
					value = TradSesReqID;
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
				case "TradSesMethod":
				{
					value = TradSesMethod;
					break;
				}
				case "TradSesMode":
				{
					value = TradSesMode;
					break;
				}
				case "SubscriptionRequestType":
				{
					value = SubscriptionRequestType;
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
			TradSesReqID = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			TradSesMethod = null;
			TradSesMode = null;
			SubscriptionRequestType = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
