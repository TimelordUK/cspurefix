using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("6", FixVersion.FIX43)]
	public sealed partial class IOI : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 1, Required = true)]
		public string? IOIid {get; set;}
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 2, Required = true)]
		public string? IOITransType {get; set;}
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 3, Required = false)]
		public string? IOIRefID {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 5, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 465, Type = TagType.Int, Offset = 6, Required = false)]
		public int? QuantityType {get; set;}
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 7, Required = true)]
		public string? IOIQty {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 8, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 9, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 10, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 11, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 12, Required = false)]
		public string? IOIQltyInd {get; set;}
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 13, Required = false)]
		public bool? IOINaturalFlag {get; set;}
		
		[Group(NoOfTag = 199, Offset = 14, Required = false)]
		public IOINoIOIQualifiers[]? NoIOIQualifiers {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 15, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 16, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 17, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 19, Required = false)]
		public string? URLLink {get; set;}
		
		[Group(NoOfTag = 215, Offset = 20, Required = false)]
		public IOINoRoutingIDs[]? NoRoutingIDs {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[TagDetails(Tag = 219, Type = TagType.String, Offset = 22, Required = false)]
		public string? Benchmark {get; set;}
		
		[Component(Offset = 23, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& IOIid is not null
				&& IOITransType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Side is not null
				&& IOIQty is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (IOIid is not null) writer.WriteString(23, IOIid);
			if (IOITransType is not null) writer.WriteString(28, IOITransType);
			if (IOIRefID is not null) writer.WriteString(26, IOIRefID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
			if (IOIQty is not null) writer.WriteString(27, IOIQty);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (IOIQltyInd is not null) writer.WriteString(25, IOIQltyInd);
			if (IOINaturalFlag is not null) writer.WriteBoolean(130, IOINaturalFlag.Value);
			if (NoIOIQualifiers is not null && NoIOIQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(199, NoIOIQualifiers.Length);
				for (int i = 0; i < NoIOIQualifiers.Length; i++)
				{
					((IFixEncoder)NoIOIQualifiers[i]).Encode(writer);
				}
			}
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (NoRoutingIDs is not null && NoRoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, NoRoutingIDs.Length);
				for (int i = 0; i < NoRoutingIDs.Length; i++)
				{
					((IFixEncoder)NoRoutingIDs[i]).Encode(writer);
				}
			}
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (Benchmark is not null) writer.WriteString(219, Benchmark);
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
			IOIid = view.GetString(23);
			IOITransType = view.GetString(28);
			IOIRefID = view.GetString(26);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			Side = view.GetString(54);
			QuantityType = view.GetInt32(465);
			IOIQty = view.GetString(27);
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			Currency = view.GetString(15);
			ValidUntilTime = view.GetDateTime(62);
			IOIQltyInd = view.GetString(25);
			IOINaturalFlag = view.GetBool(130);
			if (view.GetView("NoIOIQualifiers") is IMessageView viewNoIOIQualifiers)
			{
				var count = viewNoIOIQualifiers.GroupCount();
				NoIOIQualifiers = new IOINoIOIQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoIOIQualifiers[i] = new();
					((IFixParser)NoIOIQualifiers[i]).Parse(viewNoIOIQualifiers.GetGroupInstance(i));
				}
			}
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TransactTime = view.GetDateTime(60);
			URLLink = view.GetString(149);
			if (view.GetView("NoRoutingIDs") is IMessageView viewNoRoutingIDs)
			{
				var count = viewNoRoutingIDs.GroupCount();
				NoRoutingIDs = new IOINoRoutingIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRoutingIDs[i] = new();
					((IFixParser)NoRoutingIDs[i]).Parse(viewNoRoutingIDs.GetGroupInstance(i));
				}
			}
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			Benchmark = view.GetString(219);
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
				case "IOIid":
					value = IOIid;
					break;
				case "IOITransType":
					value = IOITransType;
					break;
				case "IOIRefID":
					value = IOIRefID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "Side":
					value = Side;
					break;
				case "QuantityType":
					value = QuantityType;
					break;
				case "IOIQty":
					value = IOIQty;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "Price":
					value = Price;
					break;
				case "Currency":
					value = Currency;
					break;
				case "ValidUntilTime":
					value = ValidUntilTime;
					break;
				case "IOIQltyInd":
					value = IOIQltyInd;
					break;
				case "IOINaturalFlag":
					value = IOINaturalFlag;
					break;
				case "NoIOIQualifiers":
					value = NoIOIQualifiers;
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
				case "TransactTime":
					value = TransactTime;
					break;
				case "URLLink":
					value = URLLink;
					break;
				case "NoRoutingIDs":
					value = NoRoutingIDs;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "Benchmark":
					value = Benchmark;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			IOIid = null;
			IOITransType = null;
			IOIRefID = null;
			((IFixReset?)Instrument)?.Reset();
			Side = null;
			QuantityType = null;
			IOIQty = null;
			PriceType = null;
			Price = null;
			Currency = null;
			ValidUntilTime = null;
			IOIQltyInd = null;
			IOINaturalFlag = null;
			NoIOIQualifiers = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			TransactTime = null;
			URLLink = null;
			NoRoutingIDs = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			Benchmark = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
