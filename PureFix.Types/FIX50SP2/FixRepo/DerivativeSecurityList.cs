using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("DerivativeSecurityList", FixVersion.FIX50SP2)]
	public sealed partial class DerivativeSecurityList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecurityResponseID {get; set;}
		
		[TagDetails(Tag = 560, Type = TagType.Int, Offset = 4, Required = false)]
		public int? SecurityRequestResult {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public DerivativeSecurityDefinitionComponent? DerivativeSecurityDefinition {get; set;}
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 7, Required = false)]
		public int? TotNoRelatedSym {get; set;}
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? LastFragment {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[TagDetails(Tag = 964, Type = TagType.Int, Offset = 10, Required = false)]
		public int? SecurityReportID {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 12, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityRequestResult is not null) writer.WriteWholeNumber(560, SecurityRequestResult.Value);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (DerivativeSecurityDefinition is not null) ((IFixEncoder)DerivativeSecurityDefinition).Encode(writer);
			if (TotNoRelatedSym is not null) writer.WriteWholeNumber(393, TotNoRelatedSym.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (SecurityReportID is not null) writer.WriteWholeNumber(964, SecurityReportID.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
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
			SecurityReqID = view.GetString(320);
			SecurityResponseID = view.GetString(322);
			SecurityRequestResult = view.GetInt32(560);
			if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
			{
				UnderlyingInstrument = new();
				((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
			}
			if (view.GetView("DerivativeSecurityDefinition") is IMessageView viewDerivativeSecurityDefinition)
			{
				DerivativeSecurityDefinition = new();
				((IFixParser)DerivativeSecurityDefinition).Parse(viewDerivativeSecurityDefinition);
			}
			TotNoRelatedSym = view.GetInt32(393);
			LastFragment = view.GetBool(893);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			SecurityReportID = view.GetInt32(964);
			ClearingBusinessDate = view.GetDateOnly(715);
			TransactTime = view.GetDateTime(60);
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
				case "SecurityReqID":
					value = SecurityReqID;
					break;
				case "SecurityResponseID":
					value = SecurityResponseID;
					break;
				case "SecurityRequestResult":
					value = SecurityRequestResult;
					break;
				case "UnderlyingInstrument":
					value = UnderlyingInstrument;
					break;
				case "DerivativeSecurityDefinition":
					value = DerivativeSecurityDefinition;
					break;
				case "TotNoRelatedSym":
					value = TotNoRelatedSym;
					break;
				case "LastFragment":
					value = LastFragment;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				case "SecurityReportID":
					value = SecurityReportID;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			SecurityReqID = null;
			SecurityResponseID = null;
			SecurityRequestResult = null;
			((IFixReset?)UnderlyingInstrument)?.Reset();
			((IFixReset?)DerivativeSecurityDefinition)?.Reset();
			TotNoRelatedSym = null;
			LastFragment = null;
			((IFixReset?)StandardTrailer)?.Reset();
			SecurityReportID = null;
			ClearingBusinessDate = null;
			TransactTime = null;
		}
	}
}
