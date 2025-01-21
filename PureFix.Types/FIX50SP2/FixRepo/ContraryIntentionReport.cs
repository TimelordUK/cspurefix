using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("ContraryIntentionReport", FixVersion.FIX50SP2)]
	public sealed partial class ContraryIntentionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 977, Type = TagType.String, Offset = 2, Required = true)]
		public string? ContIntRptID {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 3, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 978, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? LateIndicator {get; set;}
		
		[TagDetails(Tag = 979, Type = TagType.String, Offset = 5, Required = false)]
		public string? InputSource {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 6, Required = true)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 7, Required = true)]
		public ContraryIntentionReportParties[]? Parties {get; set;}
		
		[Group(NoOfTag = 1027, Offset = 8, Required = true)]
		public ContraryIntentionReportExpirationQty[]? ExpirationQty {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 10, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 14, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ContIntRptID is not null
				&& ClearingBusinessDate is not null
				&& Parties is not null && FixValidator.IsValid(Parties, in config)
				&& ExpirationQty is not null && FixValidator.IsValid(ExpirationQty, in config)
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (ContIntRptID is not null) writer.WriteString(977, ContIntRptID);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (LateIndicator is not null) writer.WriteBoolean(978, LateIndicator.Value);
			if (InputSource is not null) writer.WriteString(979, InputSource);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (ExpirationQty is not null && ExpirationQty.Length != 0)
			{
				writer.WriteWholeNumber(1027, ExpirationQty.Length);
				for (int i = 0; i < ExpirationQty.Length; i++)
				{
					((IFixEncoder)ExpirationQty[i]).Encode(writer);
				}
			}
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
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
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			ContIntRptID = view.GetString(977);
			TransactTime = view.GetDateTime(60);
			LateIndicator = view.GetBool(978);
			InputSource = view.GetString(979);
			ClearingBusinessDate = view.GetDateOnly(715);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new ContraryIntentionReportParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			if (view.GetView("ExpirationQty") is IMessageView viewExpirationQty)
			{
				var count = viewExpirationQty.GroupCount();
				ExpirationQty = new ContraryIntentionReportExpirationQty[count];
				for (int i = 0; i < count; i++)
				{
					ExpirationQty[i] = new();
					((IFixParser)ExpirationQty[i]).Parse(viewExpirationQty.GetGroupInstance(i));
				}
			}
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
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
				case "ApplicationSequenceControl":
					value = ApplicationSequenceControl;
					break;
				case "ContIntRptID":
					value = ContIntRptID;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "LateIndicator":
					value = LateIndicator;
					break;
				case "InputSource":
					value = InputSource;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "Parties":
					value = Parties;
					break;
				case "ExpirationQty":
					value = ExpirationQty;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
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
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			ContIntRptID = null;
			TransactTime = null;
			LateIndicator = null;
			InputSource = null;
			ClearingBusinessDate = null;
			Parties = null;
			ExpirationQty = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
