using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AZ", FixVersion.FIX44)]
	public sealed partial class CollateralResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 904, Type = TagType.String, Offset = 1, Required = true)]
		public string? CollRespID {get; set;}
		
		[TagDetails(Tag = 902, Type = TagType.String, Offset = 2, Required = true)]
		public string? CollAsgnID {get; set;}
		
		[TagDetails(Tag = 894, Type = TagType.String, Offset = 3, Required = false)]
		public string? CollReqID {get; set;}
		
		[TagDetails(Tag = 895, Type = TagType.Int, Offset = 4, Required = true)]
		public int? CollAsgnReason {get; set;}
		
		[TagDetails(Tag = 903, Type = TagType.Int, Offset = 5, Required = false)]
		public int? CollAsgnTransType {get; set;}
		
		[TagDetails(Tag = 905, Type = TagType.Int, Offset = 6, Required = true)]
		public int? CollAsgnRespType {get; set;}
		
		[TagDetails(Tag = 906, Type = TagType.Int, Offset = 7, Required = false)]
		public int? CollAsgnRejectReason {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 8, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 10, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 11, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 12, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 13, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 14, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[Component(Offset = 16, Required = false)]
		public ExecCollGrpComponent? ExecCollGrp {get; set;}
		
		[Component(Offset = 17, Required = false)]
		public TrdCollGrpComponent? TrdCollGrp {get; set;}
		
		[Component(Offset = 18, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 20, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 21, Required = false)]
		public double? Quantity {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 22, Required = false)]
		public int? QtyType {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 23, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 24, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[Component(Offset = 25, Required = false)]
		public UndInstrmtCollGrpComponent? UndInstrmtCollGrp {get; set;}
		
		[TagDetails(Tag = 899, Type = TagType.Float, Offset = 26, Required = false)]
		public double? MarginExcess {get; set;}
		
		[TagDetails(Tag = 900, Type = TagType.Float, Offset = 27, Required = false)]
		public double? TotalNetValue {get; set;}
		
		[TagDetails(Tag = 901, Type = TagType.Float, Offset = 28, Required = false)]
		public double? CashOutstanding {get; set;}
		
		[Component(Offset = 29, Required = false)]
		public TrdRegTimestampsComponent? TrdRegTimestamps {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30, Required = false)]
		public string? Side {get; set;}
		
		[Component(Offset = 31, Required = false)]
		public MiscFeesGrpComponent? MiscFeesGrp {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 32, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 33, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 34, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 35, Required = false)]
		public double? EndAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 36, Required = false)]
		public double? StartCash {get; set;}
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 37, Required = false)]
		public double? EndCash {get; set;}
		
		[Component(Offset = 38, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 39, Required = false)]
		public StipulationsComponent? Stipulations {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 40, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 41, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 42, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 43, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& CollRespID is not null
				&& CollAsgnID is not null
				&& CollAsgnReason is not null
				&& CollAsgnRespType is not null
				&& TransactTime is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (CollRespID is not null) writer.WriteString(904, CollRespID);
			if (CollAsgnID is not null) writer.WriteString(902, CollAsgnID);
			if (CollReqID is not null) writer.WriteString(894, CollReqID);
			if (CollAsgnReason is not null) writer.WriteWholeNumber(895, CollAsgnReason.Value);
			if (CollAsgnTransType is not null) writer.WriteWholeNumber(903, CollAsgnTransType.Value);
			if (CollAsgnRespType is not null) writer.WriteWholeNumber(905, CollAsgnRespType.Value);
			if (CollAsgnRejectReason is not null) writer.WriteWholeNumber(906, CollAsgnRejectReason.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ExecCollGrp is not null) ((IFixEncoder)ExecCollGrp).Encode(writer);
			if (TrdCollGrp is not null) ((IFixEncoder)TrdCollGrp).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtCollGrp is not null) ((IFixEncoder)UndInstrmtCollGrp).Encode(writer);
			if (MarginExcess is not null) writer.WriteNumber(899, MarginExcess.Value);
			if (TotalNetValue is not null) writer.WriteNumber(900, TotalNetValue.Value);
			if (CashOutstanding is not null) writer.WriteNumber(901, CashOutstanding.Value);
			if (TrdRegTimestamps is not null) ((IFixEncoder)TrdRegTimestamps).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (MiscFeesGrp is not null) ((IFixEncoder)MiscFeesGrp).Encode(writer);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
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
			CollRespID = view.GetString(904);
			CollAsgnID = view.GetString(902);
			CollReqID = view.GetString(894);
			CollAsgnReason = view.GetInt32(895);
			CollAsgnTransType = view.GetInt32(903);
			CollAsgnRespType = view.GetInt32(905);
			CollAsgnRejectReason = view.GetInt32(906);
			TransactTime = view.GetDateTime(60);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			Account = view.GetString(1);
			AccountType = view.GetInt32(581);
			ClOrdID = view.GetString(11);
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			SecondaryClOrdID = view.GetString(526);
			if (view.GetView("ExecCollGrp") is IMessageView viewExecCollGrp)
			{
				ExecCollGrp = new();
				((IFixParser)ExecCollGrp).Parse(viewExecCollGrp);
			}
			if (view.GetView("TrdCollGrp") is IMessageView viewTrdCollGrp)
			{
				TrdCollGrp = new();
				((IFixParser)TrdCollGrp).Parse(viewTrdCollGrp);
			}
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
			SettlDate = view.GetDateOnly(64);
			Quantity = view.GetDouble(53);
			QtyType = view.GetInt32(854);
			Currency = view.GetString(15);
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			if (view.GetView("UndInstrmtCollGrp") is IMessageView viewUndInstrmtCollGrp)
			{
				UndInstrmtCollGrp = new();
				((IFixParser)UndInstrmtCollGrp).Parse(viewUndInstrmtCollGrp);
			}
			MarginExcess = view.GetDouble(899);
			TotalNetValue = view.GetDouble(900);
			CashOutstanding = view.GetDouble(901);
			if (view.GetView("TrdRegTimestamps") is IMessageView viewTrdRegTimestamps)
			{
				TrdRegTimestamps = new();
				((IFixParser)TrdRegTimestamps).Parse(viewTrdRegTimestamps);
			}
			Side = view.GetString(54);
			if (view.GetView("MiscFeesGrp") is IMessageView viewMiscFeesGrp)
			{
				MiscFeesGrp = new();
				((IFixParser)MiscFeesGrp).Parse(viewMiscFeesGrp);
			}
			Price = view.GetDouble(44);
			PriceType = view.GetInt32(423);
			AccruedInterestAmt = view.GetDouble(159);
			EndAccruedInterestAmt = view.GetDouble(920);
			StartCash = view.GetDouble(921);
			EndCash = view.GetDouble(922);
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
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
				case "CollRespID":
					value = CollRespID;
					break;
				case "CollAsgnID":
					value = CollAsgnID;
					break;
				case "CollReqID":
					value = CollReqID;
					break;
				case "CollAsgnReason":
					value = CollAsgnReason;
					break;
				case "CollAsgnTransType":
					value = CollAsgnTransType;
					break;
				case "CollAsgnRespType":
					value = CollAsgnRespType;
					break;
				case "CollAsgnRejectReason":
					value = CollAsgnRejectReason;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "Parties":
					value = Parties;
					break;
				case "Account":
					value = Account;
					break;
				case "AccountType":
					value = AccountType;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "SecondaryOrderID":
					value = SecondaryOrderID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "ExecCollGrp":
					value = ExecCollGrp;
					break;
				case "TrdCollGrp":
					value = TrdCollGrp;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "SettlDate":
					value = SettlDate;
					break;
				case "Quantity":
					value = Quantity;
					break;
				case "QtyType":
					value = QtyType;
					break;
				case "Currency":
					value = Currency;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "UndInstrmtCollGrp":
					value = UndInstrmtCollGrp;
					break;
				case "MarginExcess":
					value = MarginExcess;
					break;
				case "TotalNetValue":
					value = TotalNetValue;
					break;
				case "CashOutstanding":
					value = CashOutstanding;
					break;
				case "TrdRegTimestamps":
					value = TrdRegTimestamps;
					break;
				case "Side":
					value = Side;
					break;
				case "MiscFeesGrp":
					value = MiscFeesGrp;
					break;
				case "Price":
					value = Price;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "AccruedInterestAmt":
					value = AccruedInterestAmt;
					break;
				case "EndAccruedInterestAmt":
					value = EndAccruedInterestAmt;
					break;
				case "StartCash":
					value = StartCash;
					break;
				case "EndCash":
					value = EndCash;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "Stipulations":
					value = Stipulations;
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
