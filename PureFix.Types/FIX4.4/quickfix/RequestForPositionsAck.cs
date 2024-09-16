using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AO", FixVersion.FIX44)]
	public sealed partial class RequestForPositionsAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID {get; set;}
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 2, Required = false)]
		public string? PosReqID {get; set;}
		
		[TagDetails(Tag = 727, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TotalNumPosReports {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 728, Type = TagType.Int, Offset = 5, Required = true)]
		public int? PosReqResult {get; set;}
		
		[TagDetails(Tag = 729, Type = TagType.Int, Offset = 6, Required = true)]
		public int? PosReqStatus {get; set;}
		
		[Component(Offset = 7, Required = true)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = true)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10, Required = true)]
		public int? AccountType {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 12, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 15, Required = false)]
		public int? ResponseTransportType {get; set;}
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 16, Required = false)]
		public string? ResponseDestination {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 17, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 20, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PosMaintRptID is not null
				&& PosReqResult is not null
				&& PosReqStatus is not null
				&& Parties is not null && ((IFixValidator)Parties).IsValid(in config)
				&& Account is not null
				&& AccountType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PosMaintRptID is not null) writer.WriteString(721, PosMaintRptID);
			if (PosReqID is not null) writer.WriteString(710, PosReqID);
			if (TotalNumPosReports is not null) writer.WriteWholeNumber(727, TotalNumPosReports.Value);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (PosReqResult is not null) writer.WriteWholeNumber(728, PosReqResult.Value);
			if (PosReqStatus is not null) writer.WriteWholeNumber(729, PosReqStatus.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
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
			PosReqID = view.GetString(710);
			TotalNumPosReports = view.GetInt32(727);
			UnsolicitedIndicator = view.GetBool(325);
			PosReqResult = view.GetInt32(728);
			PosReqStatus = view.GetInt32(729);
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
			ResponseTransportType = view.GetInt32(725);
			ResponseDestination = view.GetString(726);
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
				case "PosReqID":
					value = PosReqID;
					break;
				case "TotalNumPosReports":
					value = TotalNumPosReports;
					break;
				case "UnsolicitedIndicator":
					value = UnsolicitedIndicator;
					break;
				case "PosReqResult":
					value = PosReqResult;
					break;
				case "PosReqStatus":
					value = PosReqStatus;
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
				case "ResponseTransportType":
					value = ResponseTransportType;
					break;
				case "ResponseDestination":
					value = ResponseDestination;
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
