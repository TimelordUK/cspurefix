using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("6", FixVersion.FIX44)]
	public sealed partial class IOI : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 1, Required = true)]
		public string? IOIID {get; set;}
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 2, Required = true)]
		public string? IOITransType {get; set;}
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 3, Required = false)]
		public string? IOIRefID {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 7, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 8, Required = false)]
		public int? QtyType {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 10, Required = true)]
		public string? IOIQty {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 11, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public StipulationsComponent? Stipulations {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegIOIGrpComponent? InstrmtLegIOIGrp {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 14, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 15, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 16, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 17, Required = false)]
		public string? IOIQltyInd {get; set;}
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 18, Required = false)]
		public bool? IOINaturalFlag {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public IOIQualGrpComponent? IOIQualGrp {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 20, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 21, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 22, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 23, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 24, Required = false)]
		public string? URLLink {get; set;}
		
		[Component(Offset = 25, Required = false)]
		public RoutingGrpComponent? RoutingGrp {get; set;}
		
		[Component(Offset = 26, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 27, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[Component(Offset = 28, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& IOIID is not null
				&& IOITransType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Side is not null
				&& IOIQty is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (IOIID is not null) writer.WriteString(23, IOIID);
			if (IOITransType is not null) writer.WriteString(28, IOITransType);
			if (IOIRefID is not null) writer.WriteString(26, IOIRefID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (IOIQty is not null) writer.WriteString(27, IOIQty);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (InstrmtLegIOIGrp is not null) ((IFixEncoder)InstrmtLegIOIGrp).Encode(writer);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (IOIQltyInd is not null) writer.WriteString(25, IOIQltyInd);
			if (IOINaturalFlag is not null) writer.WriteBoolean(130, IOINaturalFlag.Value);
			if (IOIQualGrp is not null) ((IFixEncoder)IOIQualGrp).Encode(writer);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (RoutingGrp is not null) ((IFixEncoder)RoutingGrp).Encode(writer);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
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
			IOIID = view.GetString(23);
			IOITransType = view.GetString(28);
			IOIRefID = view.GetString(26);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
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
			Side = view.GetString(54);
			QtyType = view.GetInt32(854);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			IOIQty = view.GetString(27);
			Currency = view.GetString(15);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
			if (view.GetView("InstrmtLegIOIGrp") is IMessageView viewInstrmtLegIOIGrp)
			{
				InstrmtLegIOIGrp = new();
				((IFixParser)InstrmtLegIOIGrp).Parse(viewInstrmtLegIOIGrp);
			}
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			ValidUntilTime = view.GetDateTime(62);
			IOIQltyInd = view.GetString(25);
			IOINaturalFlag = view.GetBool(130);
			if (view.GetView("IOIQualGrp") is IMessageView viewIOIQualGrp)
			{
				IOIQualGrp = new();
				((IFixParser)IOIQualGrp).Parse(viewIOIQualGrp);
			}
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TransactTime = view.GetDateTime(60);
			URLLink = view.GetString(149);
			if (view.GetView("RoutingGrp") is IMessageView viewRoutingGrp)
			{
				RoutingGrp = new();
				((IFixParser)RoutingGrp).Parse(viewRoutingGrp);
			}
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
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
					value = StandardHeader;
					break;
				case "IOIID":
					value = IOIID;
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
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "Side":
					value = Side;
					break;
				case "QtyType":
					value = QtyType;
					break;
				case "OrderQtyData":
					value = OrderQtyData;
					break;
				case "IOIQty":
					value = IOIQty;
					break;
				case "Currency":
					value = Currency;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "InstrmtLegIOIGrp":
					value = InstrmtLegIOIGrp;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "Price":
					value = Price;
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
				case "IOIQualGrp":
					value = IOIQualGrp;
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
				case "RoutingGrp":
					value = RoutingGrp;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "YieldData":
					value = YieldData;
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
