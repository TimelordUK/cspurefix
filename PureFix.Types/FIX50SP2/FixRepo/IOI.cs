using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("IOI", FixVersion.FIX50SP2)]
	public sealed partial class IOI : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 2, Required = true)]
		public string? IOIID {get; set;}
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 3, Required = true)]
		public string? IOITransType {get; set;}
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 4, Required = false)]
		public string? IOIRefID {get; set;}
		
		[Component(Offset = 5, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 6, Required = false)]
		public IOIParties[]? Parties {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 8, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 9, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 10, Required = false)]
		public int? QtyType {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 12, Required = true)]
		public string? IOIQty {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 13, Required = false)]
		public string? Currency {get; set;}
		
		[Group(NoOfTag = 1019, Offset = 14, Required = false)]
		public IOIStipulations[]? Stipulations {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 15, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 16, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 17, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 18, Required = false)]
		public string? IOIQltyInd {get; set;}
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 19, Required = false)]
		public bool? IOINaturalFlag {get; set;}
		
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
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 26, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[Component(Offset = 27, Required = true)]
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
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (IOIID is not null) writer.WriteString(23, IOIID);
			if (IOITransType is not null) writer.WriteString(28, IOITransType);
			if (IOIRefID is not null) writer.WriteString(26, IOIRefID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (IOIQty is not null) writer.WriteString(27, IOIQty);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (Stipulations is not null && Stipulations.Length != 0)
			{
				writer.WriteWholeNumber(1019, Stipulations.Length);
				for (int i = 0; i < Stipulations.Length; i++)
				{
					((IFixEncoder)Stipulations[i]).Encode(writer);
				}
			}
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (IOIQltyInd is not null) writer.WriteString(25, IOIQltyInd);
			if (IOINaturalFlag is not null) writer.WriteBoolean(130, IOINaturalFlag.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (URLLink is not null) writer.WriteString(149, URLLink);
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
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			IOIID = view.GetString(23);
			IOITransType = view.GetString(28);
			IOIRefID = view.GetString(26);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new IOIParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
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
				var count = viewStipulations.GroupCount();
				Stipulations = new IOIStipulations[count];
				for (int i = 0; i < count; i++)
				{
					Stipulations[i] = new();
					((IFixParser)Stipulations[i]).Parse(viewStipulations.GetGroupInstance(i));
				}
			}
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			ValidUntilTime = view.GetDateTime(62);
			IOIQltyInd = view.GetString(25);
			IOINaturalFlag = view.GetBool(130);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TransactTime = view.GetDateTime(60);
			URLLink = view.GetString(149);
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
				case "ApplicationSequenceControl":
					value = ApplicationSequenceControl;
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
				case "Parties":
					value = Parties;
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
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			IOIID = null;
			IOITransType = null;
			IOIRefID = null;
			((IFixReset?)Instrument)?.Reset();
			Parties = null;
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Side = null;
			QtyType = null;
			((IFixReset?)OrderQtyData)?.Reset();
			IOIQty = null;
			Currency = null;
			Stipulations = null;
			PriceType = null;
			Price = null;
			ValidUntilTime = null;
			IOIQltyInd = null;
			IOINaturalFlag = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			TransactTime = null;
			URLLink = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
