using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AL", FixVersion.FIX44)]
	public sealed partial class PositionMaintenanceRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosReqID {get; set;}
		
		[TagDetails(Tag = 709, Type = TagType.Int, Offset = 2, Required = true)]
		public int? PosTransType {get; set;}
		
		[TagDetails(Tag = 712, Type = TagType.Int, Offset = 3, Required = true)]
		public int? PosMaintAction {get; set;}
		
		[TagDetails(Tag = 713, Type = TagType.String, Offset = 4, Required = false)]
		public string? OrigPosReqRefID {get; set;}
		
		[TagDetails(Tag = 714, Type = TagType.String, Offset = 5, Required = false)]
		public string? PosMaintRptRefID {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 6, Required = true)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 7, Required = false)]
		public string? SettlSessID {get; set;}
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 8, Required = false)]
		public string? SettlSessSubID {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 10, Required = true)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 11, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 12, Required = true)]
		public int? AccountType {get; set;}
		
		[Component(Offset = 13, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 14, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 15, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[Component(Offset = 16, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 17, Required = false)]
		public TrdgSesGrpComponent? TrdgSesGrp {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 19, Required = true)]
		public PositionQtyComponent? PositionQty {get; set;}
		
		[TagDetails(Tag = 718, Type = TagType.Int, Offset = 20, Required = false)]
		public int? AdjustmentType {get; set;}
		
		[TagDetails(Tag = 719, Type = TagType.Boolean, Offset = 21, Required = false)]
		public bool? ContraryInstructionIndicator {get; set;}
		
		[TagDetails(Tag = 720, Type = TagType.Boolean, Offset = 22, Required = false)]
		public bool? PriorSpreadIndicator {get; set;}
		
		[TagDetails(Tag = 834, Type = TagType.Float, Offset = 23, Required = false)]
		public double? ThresholdAmount {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 27, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PosReqID is not null
				&& PosTransType is not null
				&& PosMaintAction is not null
				&& ClearingBusinessDate is not null
				&& Parties is not null && ((IFixValidator)Parties).IsValid(in config)
				&& Account is not null
				&& AccountType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& TransactTime is not null
				&& PositionQty is not null && ((IFixValidator)PositionQty).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PosReqID is not null) writer.WriteString(710, PosReqID);
			if (PosTransType is not null) writer.WriteWholeNumber(709, PosTransType.Value);
			if (PosMaintAction is not null) writer.WriteWholeNumber(712, PosMaintAction.Value);
			if (OrigPosReqRefID is not null) writer.WriteString(713, OrigPosReqRefID);
			if (PosMaintRptRefID is not null) writer.WriteString(714, PosMaintRptRefID);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (TrdgSesGrp is not null) ((IFixEncoder)TrdgSesGrp).Encode(writer);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (PositionQty is not null) ((IFixEncoder)PositionQty).Encode(writer);
			if (AdjustmentType is not null) writer.WriteWholeNumber(718, AdjustmentType.Value);
			if (ContraryInstructionIndicator is not null) writer.WriteBoolean(719, ContraryInstructionIndicator.Value);
			if (PriorSpreadIndicator is not null) writer.WriteBoolean(720, PriorSpreadIndicator.Value);
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
			PosReqID = view.GetString(710);
			PosTransType = view.GetInt32(709);
			PosMaintAction = view.GetInt32(712);
			OrigPosReqRefID = view.GetString(713);
			PosMaintRptRefID = view.GetString(714);
			ClearingBusinessDate = view.GetDateOnly(715);
			SettlSessID = view.GetString(716);
			SettlSessSubID = view.GetString(717);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			Currency = view.GetString(15);
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
			if (view.GetView("TrdgSesGrp") is IMessageView viewTrdgSesGrp)
			{
				TrdgSesGrp = new();
				((IFixParser)TrdgSesGrp).Parse(viewTrdgSesGrp);
			}
			TransactTime = view.GetDateTime(60);
			if (view.GetView("PositionQty") is IMessageView viewPositionQty)
			{
				PositionQty = new();
				((IFixParser)PositionQty).Parse(viewPositionQty);
			}
			AdjustmentType = view.GetInt32(718);
			ContraryInstructionIndicator = view.GetBool(719);
			PriorSpreadIndicator = view.GetBool(720);
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
				case "PosReqID":
					value = PosReqID;
					break;
				case "PosTransType":
					value = PosTransType;
					break;
				case "PosMaintAction":
					value = PosMaintAction;
					break;
				case "OrigPosReqRefID":
					value = OrigPosReqRefID;
					break;
				case "PosMaintRptRefID":
					value = PosMaintRptRefID;
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
				case "Instrument":
					value = Instrument;
					break;
				case "Currency":
					value = Currency;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "TrdgSesGrp":
					value = TrdgSesGrp;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "PositionQty":
					value = PositionQty;
					break;
				case "AdjustmentType":
					value = AdjustmentType;
					break;
				case "ContraryInstructionIndicator":
					value = ContraryInstructionIndicator;
					break;
				case "PriorSpreadIndicator":
					value = PriorSpreadIndicator;
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
	}
}
