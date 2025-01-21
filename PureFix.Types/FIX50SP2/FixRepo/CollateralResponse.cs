using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("CollateralResponse", FixVersion.FIX50SP2)]
	public sealed partial class CollateralResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 904, Type = TagType.String, Offset = 1, Required = true)]
		public string? CollRespID {get; set;}
		
		[TagDetails(Tag = 902, Type = TagType.String, Offset = 2, Required = false)]
		public string? CollAsgnID {get; set;}
		
		[TagDetails(Tag = 894, Type = TagType.String, Offset = 3, Required = false)]
		public string? CollReqID {get; set;}
		
		[TagDetails(Tag = 895, Type = TagType.Int, Offset = 4, Required = false)]
		public int? CollAsgnReason {get; set;}
		
		[TagDetails(Tag = 903, Type = TagType.Int, Offset = 5, Required = false)]
		public int? CollAsgnTransType {get; set;}
		
		[TagDetails(Tag = 905, Type = TagType.Int, Offset = 6, Required = true)]
		public int? CollAsgnRespType {get; set;}
		
		[TagDetails(Tag = 906, Type = TagType.Int, Offset = 7, Required = false)]
		public int? CollAsgnRejectReason {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 8, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 1043, Type = TagType.Int, Offset = 9, Required = false)]
		public int? CollApplType {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 10, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 12, Required = false)]
		public CollateralResponseParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 13, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 14, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 15, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 16, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 17, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 20, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 21, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 22, Required = false)]
		public double? Quantity {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 23, Required = false)]
		public int? QtyType {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 24, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 25, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 899, Type = TagType.Float, Offset = 26, Required = false)]
		public double? MarginExcess {get; set;}
		
		[TagDetails(Tag = 900, Type = TagType.Float, Offset = 27, Required = false)]
		public double? TotalNetValue {get; set;}
		
		[TagDetails(Tag = 901, Type = TagType.Float, Offset = 28, Required = false)]
		public double? CashOutstanding {get; set;}
		
		[Group(NoOfTag = 1020, Offset = 29, Required = false)]
		public CollateralResponseTrdRegTimestamps[]? TrdRegTimestamps {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30, Required = false)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 31, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 32, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 33, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 34, Required = false)]
		public double? EndAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 35, Required = false)]
		public double? StartCash {get; set;}
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 36, Required = false)]
		public double? EndCash {get; set;}
		
		[Component(Offset = 37, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Group(NoOfTag = 1019, Offset = 38, Required = false)]
		public CollateralResponseStipulations[]? Stipulations {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 39, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 40, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 41, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 42, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& CollRespID is not null
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
			if (CollApplType is not null) writer.WriteWholeNumber(1043, CollApplType.Value);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (MarginExcess is not null) writer.WriteNumber(899, MarginExcess.Value);
			if (TotalNetValue is not null) writer.WriteNumber(900, TotalNetValue.Value);
			if (CashOutstanding is not null) writer.WriteNumber(901, CashOutstanding.Value);
			if (TrdRegTimestamps is not null && TrdRegTimestamps.Length != 0)
			{
				writer.WriteWholeNumber(1020, TrdRegTimestamps.Length);
				for (int i = 0; i < TrdRegTimestamps.Length; i++)
				{
					((IFixEncoder)TrdRegTimestamps[i]).Encode(writer);
				}
			}
			if (Side is not null) writer.WriteString(54, Side);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (Stipulations is not null && Stipulations.Length != 0)
			{
				writer.WriteWholeNumber(1019, Stipulations.Length);
				for (int i = 0; i < Stipulations.Length; i++)
				{
					((IFixEncoder)Stipulations[i]).Encode(writer);
				}
			}
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
			CollApplType = view.GetInt32(1043);
			FinancialStatus = view.GetString(291);
			ClearingBusinessDate = view.GetDateOnly(715);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new CollateralResponseParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			Account = view.GetString(1);
			AccountType = view.GetInt32(581);
			ClOrdID = view.GetString(11);
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			SecondaryClOrdID = view.GetString(526);
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
			MarginExcess = view.GetDouble(899);
			TotalNetValue = view.GetDouble(900);
			CashOutstanding = view.GetDouble(901);
			if (view.GetView("TrdRegTimestamps") is IMessageView viewTrdRegTimestamps)
			{
				var count = viewTrdRegTimestamps.GroupCount();
				TrdRegTimestamps = new CollateralResponseTrdRegTimestamps[count];
				for (int i = 0; i < count; i++)
				{
					TrdRegTimestamps[i] = new();
					((IFixParser)TrdRegTimestamps[i]).Parse(viewTrdRegTimestamps.GetGroupInstance(i));
				}
			}
			Side = view.GetString(54);
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
				var count = viewStipulations.GroupCount();
				Stipulations = new CollateralResponseStipulations[count];
				for (int i = 0; i < count; i++)
				{
					Stipulations[i] = new();
					((IFixParser)Stipulations[i]).Parse(viewStipulations.GetGroupInstance(i));
				}
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
				case "CollApplType":
					value = CollApplType;
					break;
				case "FinancialStatus":
					value = FinancialStatus;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
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
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			CollRespID = null;
			CollAsgnID = null;
			CollReqID = null;
			CollAsgnReason = null;
			CollAsgnTransType = null;
			CollAsgnRespType = null;
			CollAsgnRejectReason = null;
			TransactTime = null;
			CollApplType = null;
			FinancialStatus = null;
			ClearingBusinessDate = null;
			Parties = null;
			Account = null;
			AccountType = null;
			ClOrdID = null;
			OrderID = null;
			SecondaryOrderID = null;
			SecondaryClOrdID = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			SettlDate = null;
			Quantity = null;
			QtyType = null;
			Currency = null;
			((IFixReset?)InstrmtLegGrp)?.Reset();
			MarginExcess = null;
			TotalNetValue = null;
			CashOutstanding = null;
			TrdRegTimestamps = null;
			Side = null;
			Price = null;
			PriceType = null;
			AccruedInterestAmt = null;
			EndAccruedInterestAmt = null;
			StartCash = null;
			EndCash = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			Stipulations = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
