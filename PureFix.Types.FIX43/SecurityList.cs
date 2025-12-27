using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("y", FixVersion.FIX43)]
	public sealed partial class SecurityList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID {get; set;}
		
		[TagDetails(Tag = 560, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityRequestResult {get; set;}
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TotalNumSecurities {get; set;}
		
		public sealed partial class NoRelatedSym : IFixGroup
		{
			[Component(Offset = 0, Required = false)]
			public Instrument? Instrument {get; set;}
			
			[TagDetails(Tag = 15, Type = TagType.String, Offset = 1, Required = false)]
			public string? Currency {get; set;}
			
			public sealed partial class NoLegs : IFixGroup
			{
				[Component(Offset = 0, Required = false)]
				public InstrumentLeg? InstrumentLeg {get; set;}
				
				[TagDetails(Tag = 556, Type = TagType.String, Offset = 1, Required = false)]
				public string? LegCurrency {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
					if (LegCurrency is not null) writer.WriteString(556, LegCurrency);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
					{
						InstrumentLeg = new();
						((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
					}
					LegCurrency = view.GetString(556);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "InstrumentLeg":
						{
							value = InstrumentLeg;
							break;
						}
						case "LegCurrency":
						{
							value = LegCurrency;
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
					((IFixReset?)InstrumentLeg)?.Reset();
					LegCurrency = null;
				}
			}
			[Group(NoOfTag = 555, Offset = 2, Required = false)]
			public NoLegs[]? Legs {get; set;}
			
			[TagDetails(Tag = 561, Type = TagType.Float, Offset = 3, Required = false)]
			public double? RoundLot {get; set;}
			
			[TagDetails(Tag = 562, Type = TagType.Float, Offset = 4, Required = false)]
			public double? MinTradeVol {get; set;}
			
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 5, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 6, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
				if (Currency is not null) writer.WriteString(15, Currency);
				if (Legs is not null && Legs.Length != 0)
				{
					writer.WriteWholeNumber(555, Legs.Length);
					for (int i = 0; i < Legs.Length; i++)
					{
						((IFixEncoder)Legs[i]).Encode(writer);
					}
				}
				if (RoundLot is not null) writer.WriteNumber(561, RoundLot.Value);
				if (MinTradeVol is not null) writer.WriteNumber(562, MinTradeVol.Value);
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				if (view.GetView("Instrument") is IMessageView viewInstrument)
				{
					Instrument = new();
					((IFixParser)Instrument).Parse(viewInstrument);
				}
				Currency = view.GetString(15);
				if (view.GetView("NoLegs") is IMessageView viewNoLegs)
				{
					var count = viewNoLegs.GroupCount();
					Legs = new NoLegs[count];
					for (int i = 0; i < count; i++)
					{
						Legs[i] = new();
						((IFixParser)Legs[i]).Parse(viewNoLegs.GetGroupInstance(i));
					}
				}
				RoundLot = view.GetDouble(561);
				MinTradeVol = view.GetDouble(562);
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Instrument":
					{
						value = Instrument;
						break;
					}
					case "Currency":
					{
						value = Currency;
						break;
					}
					case "NoLegs":
					{
						value = Legs;
						break;
					}
					case "RoundLot":
					{
						value = RoundLot;
						break;
					}
					case "MinTradeVol":
					{
						value = MinTradeVol;
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
					case "Text":
					{
						value = Text;
						break;
					}
					case "EncodedTextLen":
					{
						value = EncodedTextLen;
						break;
					}
					case "EncodedText":
					{
						value = EncodedText;
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
				((IFixReset?)Instrument)?.Reset();
				Currency = null;
				Legs = null;
				RoundLot = null;
				MinTradeVol = null;
				TradingSessionID = null;
				TradingSessionSubID = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 146, Offset = 5, Required = false)]
		public NoRelatedSym[]? RelatedSym {get; set;}
		
		[Component(Offset = 6, Required = true)]
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
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityRequestResult is not null) writer.WriteWholeNumber(560, SecurityRequestResult.Value);
			if (TotalNumSecurities is not null) writer.WriteWholeNumber(393, TotalNumSecurities.Value);
			if (RelatedSym is not null && RelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, RelatedSym.Length);
				for (int i = 0; i < RelatedSym.Length; i++)
				{
					((IFixEncoder)RelatedSym[i]).Encode(writer);
				}
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
			SecurityReqID = view.GetString(320);
			SecurityResponseID = view.GetString(322);
			SecurityRequestResult = view.GetInt32(560);
			TotalNumSecurities = view.GetInt32(393);
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				RelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					RelatedSym[i] = new();
					((IFixParser)RelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
			}
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
				case "SecurityReqID":
				{
					value = SecurityReqID;
					break;
				}
				case "SecurityResponseID":
				{
					value = SecurityResponseID;
					break;
				}
				case "SecurityRequestResult":
				{
					value = SecurityRequestResult;
					break;
				}
				case "TotalNumSecurities":
				{
					value = TotalNumSecurities;
					break;
				}
				case "NoRelatedSym":
				{
					value = RelatedSym;
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
			SecurityReqID = null;
			SecurityResponseID = null;
			SecurityRequestResult = null;
			TotalNumSecurities = null;
			RelatedSym = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
