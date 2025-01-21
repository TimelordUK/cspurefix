using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("PositionMaintenanceReport", FixVersion.FIX50SP2)]
	public sealed partial class PositionMaintenanceReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID {get; set;}
		
		[TagDetails(Tag = 709, Type = TagType.Int, Offset = 2, Required = true)]
		public int? PosTransType {get; set;}
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 3, Required = false)]
		public string? PosReqID {get; set;}
		
		[TagDetails(Tag = 712, Type = TagType.Int, Offset = 4, Required = true)]
		public int? PosMaintAction {get; set;}
		
		[TagDetails(Tag = 713, Type = TagType.String, Offset = 5, Required = false)]
		public string? OrigPosReqRefID {get; set;}
		
		[TagDetails(Tag = 722, Type = TagType.Int, Offset = 6, Required = true)]
		public int? PosMaintStatus {get; set;}
		
		[TagDetails(Tag = 723, Type = TagType.Int, Offset = 7, Required = false)]
		public int? PosMaintResult {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 8, Required = true)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 9, Required = false)]
		public string? SettlSessID {get; set;}
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 10, Required = false)]
		public string? SettlSessSubID {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 11, Required = false)]
		public PositionMaintenanceReportParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 12, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 13, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 14, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 714, Type = TagType.String, Offset = 15, Required = false)]
		public string? PosMaintRptRefID {get; set;}
		
		[Component(Offset = 16, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 17, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 18, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 719, Type = TagType.Boolean, Offset = 19, Required = false)]
		public bool? ContraryInstructionIndicator {get; set;}
		
		[TagDetails(Tag = 720, Type = TagType.Boolean, Offset = 20, Required = false)]
		public bool? PriorSpreadIndicator {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[Component(Offset = 22, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 23, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[Group(NoOfTag = 1015, Offset = 24, Required = true)]
		public PositionMaintenanceReportPositionQty[]? PositionQty {get; set;}
		
		[Group(NoOfTag = 1014, Offset = 25, Required = false)]
		public PositionMaintenanceReportPositionAmountData[]? PositionAmountData {get; set;}
		
		[TagDetails(Tag = 718, Type = TagType.Int, Offset = 26, Required = false)]
		public int? AdjustmentType {get; set;}
		
		[TagDetails(Tag = 834, Type = TagType.Float, Offset = 27, Required = false)]
		public double? ThresholdAmount {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 28, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 29, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 30, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 31, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PosMaintRptID is not null
				&& PosTransType is not null
				&& PosMaintAction is not null
				&& PosMaintStatus is not null
				&& ClearingBusinessDate is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& PositionQty is not null && FixValidator.IsValid(PositionQty, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PosMaintRptID is not null) writer.WriteString(721, PosMaintRptID);
			if (PosTransType is not null) writer.WriteWholeNumber(709, PosTransType.Value);
			if (PosReqID is not null) writer.WriteString(710, PosReqID);
			if (PosMaintAction is not null) writer.WriteWholeNumber(712, PosMaintAction.Value);
			if (OrigPosReqRefID is not null) writer.WriteString(713, OrigPosReqRefID);
			if (PosMaintStatus is not null) writer.WriteWholeNumber(722, PosMaintStatus.Value);
			if (PosMaintResult is not null) writer.WriteWholeNumber(723, PosMaintResult.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (PosMaintRptRefID is not null) writer.WriteString(714, PosMaintRptRefID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (ContraryInstructionIndicator is not null) writer.WriteBoolean(719, ContraryInstructionIndicator.Value);
			if (PriorSpreadIndicator is not null) writer.WriteBoolean(720, PriorSpreadIndicator.Value);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (PositionQty is not null && PositionQty.Length != 0)
			{
				writer.WriteWholeNumber(1015, PositionQty.Length);
				for (int i = 0; i < PositionQty.Length; i++)
				{
					((IFixEncoder)PositionQty[i]).Encode(writer);
				}
			}
			if (PositionAmountData is not null && PositionAmountData.Length != 0)
			{
				writer.WriteWholeNumber(1014, PositionAmountData.Length);
				for (int i = 0; i < PositionAmountData.Length; i++)
				{
					((IFixEncoder)PositionAmountData[i]).Encode(writer);
				}
			}
			if (AdjustmentType is not null) writer.WriteWholeNumber(718, AdjustmentType.Value);
			if (ThresholdAmount is not null) writer.WriteNumber(834, ThresholdAmount.Value);
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
			PosMaintRptID = view.GetString(721);
			PosTransType = view.GetInt32(709);
			PosReqID = view.GetString(710);
			PosMaintAction = view.GetInt32(712);
			OrigPosReqRefID = view.GetString(713);
			PosMaintStatus = view.GetInt32(722);
			PosMaintResult = view.GetInt32(723);
			ClearingBusinessDate = view.GetDateOnly(715);
			SettlSessID = view.GetString(716);
			SettlSessSubID = view.GetString(717);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new PositionMaintenanceReportParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			PosMaintRptRefID = view.GetString(714);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			Currency = view.GetString(15);
			SettlCurrency = view.GetString(120);
			ContraryInstructionIndicator = view.GetBool(719);
			PriorSpreadIndicator = view.GetBool(720);
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			TransactTime = view.GetDateTime(60);
			if (view.GetView("PositionQty") is IMessageView viewPositionQty)
			{
				var count = viewPositionQty.GroupCount();
				PositionQty = new PositionMaintenanceReportPositionQty[count];
				for (int i = 0; i < count; i++)
				{
					PositionQty[i] = new();
					((IFixParser)PositionQty[i]).Parse(viewPositionQty.GetGroupInstance(i));
				}
			}
			if (view.GetView("PositionAmountData") is IMessageView viewPositionAmountData)
			{
				var count = viewPositionAmountData.GroupCount();
				PositionAmountData = new PositionMaintenanceReportPositionAmountData[count];
				for (int i = 0; i < count; i++)
				{
					PositionAmountData[i] = new();
					((IFixParser)PositionAmountData[i]).Parse(viewPositionAmountData.GetGroupInstance(i));
				}
			}
			AdjustmentType = view.GetInt32(718);
			ThresholdAmount = view.GetDouble(834);
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
				case "PosMaintRptID":
					value = PosMaintRptID;
					break;
				case "PosTransType":
					value = PosTransType;
					break;
				case "PosReqID":
					value = PosReqID;
					break;
				case "PosMaintAction":
					value = PosMaintAction;
					break;
				case "OrigPosReqRefID":
					value = OrigPosReqRefID;
					break;
				case "PosMaintStatus":
					value = PosMaintStatus;
					break;
				case "PosMaintResult":
					value = PosMaintResult;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "SettlSessID":
					value = SettlSessID;
					break;
				case "SettlSessSubID":
					value = SettlSessSubID;
					break;
				case "Parties":
					value = Parties;
					break;
				case "Account":
					value = Account;
					break;
				case "AcctIDSource":
					value = AcctIDSource;
					break;
				case "AccountType":
					value = AccountType;
					break;
				case "PosMaintRptRefID":
					value = PosMaintRptRefID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "Currency":
					value = Currency;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
					break;
				case "ContraryInstructionIndicator":
					value = ContraryInstructionIndicator;
					break;
				case "PriorSpreadIndicator":
					value = PriorSpreadIndicator;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "PositionQty":
					value = PositionQty;
					break;
				case "PositionAmountData":
					value = PositionAmountData;
					break;
				case "AdjustmentType":
					value = AdjustmentType;
					break;
				case "ThresholdAmount":
					value = ThresholdAmount;
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
			PosMaintRptID = null;
			PosTransType = null;
			PosReqID = null;
			PosMaintAction = null;
			OrigPosReqRefID = null;
			PosMaintStatus = null;
			PosMaintResult = null;
			ClearingBusinessDate = null;
			SettlSessID = null;
			SettlSessSubID = null;
			Parties = null;
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			PosMaintRptRefID = null;
			((IFixReset?)Instrument)?.Reset();
			Currency = null;
			SettlCurrency = null;
			ContraryInstructionIndicator = null;
			PriorSpreadIndicator = null;
			((IFixReset?)InstrmtLegGrp)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			TransactTime = null;
			PositionQty = null;
			PositionAmountData = null;
			AdjustmentType = null;
			ThresholdAmount = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
