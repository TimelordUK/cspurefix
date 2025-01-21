using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("MarketDataSnapshotFullRefresh", FixVersion.FIX50SP2)]
	public sealed partial class MarketDataSnapshotFullRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TotNumReports {get; set;}
		
		[TagDetails(Tag = 963, Type = TagType.Int, Offset = 3, Required = false)]
		public int? MDReportID {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 1021, Type = TagType.Int, Offset = 5, Required = false)]
		public int? MDBookType {get; set;}
		
		[TagDetails(Tag = 1173, Type = TagType.Int, Offset = 6, Required = false)]
		public int? MDSubBookType {get; set;}
		
		[TagDetails(Tag = 264, Type = TagType.Int, Offset = 7, Required = false)]
		public int? MarketDepth {get; set;}
		
		[TagDetails(Tag = 1022, Type = TagType.String, Offset = 8, Required = false)]
		public string? MDFeedType {get; set;}
		
		[TagDetails(Tag = 1187, Type = TagType.Boolean, Offset = 9, Required = false)]
		public bool? RefreshIndicator {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 11, Required = false)]
		public string? MDReqID {get; set;}
		
		[Component(Offset = 12, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 15, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 16, Required = false)]
		public string? CorporateAction {get; set;}
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 17, Required = false)]
		public double? NetChgPrevDay {get; set;}
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 18, Required = false)]
		public int? ApplQueueDepth {get; set;}
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 19, Required = false)]
		public int? ApplQueueResolution {get; set;}
		
		[Component(Offset = 20, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[TagDetails(Tag = 1500, Type = TagType.String, Offset = 21, Required = false)]
		public string? MDStreamID {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (TotNumReports is not null) writer.WriteWholeNumber(911, TotNumReports.Value);
			if (MDReportID is not null) writer.WriteWholeNumber(963, MDReportID.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (MDBookType is not null) writer.WriteWholeNumber(1021, MDBookType.Value);
			if (MDSubBookType is not null) writer.WriteWholeNumber(1173, MDSubBookType.Value);
			if (MarketDepth is not null) writer.WriteWholeNumber(264, MarketDepth.Value);
			if (MDFeedType is not null) writer.WriteString(1022, MDFeedType);
			if (RefreshIndicator is not null) writer.WriteBoolean(1187, RefreshIndicator.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (NetChgPrevDay is not null) writer.WriteNumber(451, NetChgPrevDay.Value);
			if (ApplQueueDepth is not null) writer.WriteWholeNumber(813, ApplQueueDepth.Value);
			if (ApplQueueResolution is not null) writer.WriteWholeNumber(814, ApplQueueResolution.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (MDStreamID is not null) writer.WriteString(1500, MDStreamID);
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
			TotNumReports = view.GetInt32(911);
			MDReportID = view.GetInt32(963);
			ClearingBusinessDate = view.GetDateOnly(715);
			MDBookType = view.GetInt32(1021);
			MDSubBookType = view.GetInt32(1173);
			MarketDepth = view.GetInt32(264);
			MDFeedType = view.GetString(1022);
			RefreshIndicator = view.GetBool(1187);
			TradeDate = view.GetDateOnly(75);
			MDReqID = view.GetString(262);
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
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			FinancialStatus = view.GetString(291);
			CorporateAction = view.GetString(292);
			NetChgPrevDay = view.GetDouble(451);
			ApplQueueDepth = view.GetInt32(813);
			ApplQueueResolution = view.GetInt32(814);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			MDStreamID = view.GetString(1500);
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
				case "TotNumReports":
					value = TotNumReports;
					break;
				case "MDReportID":
					value = MDReportID;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "MDBookType":
					value = MDBookType;
					break;
				case "MDSubBookType":
					value = MDSubBookType;
					break;
				case "MarketDepth":
					value = MarketDepth;
					break;
				case "MDFeedType":
					value = MDFeedType;
					break;
				case "RefreshIndicator":
					value = RefreshIndicator;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "MDReqID":
					value = MDReqID;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "FinancialStatus":
					value = FinancialStatus;
					break;
				case "CorporateAction":
					value = CorporateAction;
					break;
				case "NetChgPrevDay":
					value = NetChgPrevDay;
					break;
				case "ApplQueueDepth":
					value = ApplQueueDepth;
					break;
				case "ApplQueueResolution":
					value = ApplQueueResolution;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				case "MDStreamID":
					value = MDStreamID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			TotNumReports = null;
			MDReportID = null;
			ClearingBusinessDate = null;
			MDBookType = null;
			MDSubBookType = null;
			MarketDepth = null;
			MDFeedType = null;
			RefreshIndicator = null;
			TradeDate = null;
			MDReqID = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			((IFixReset?)InstrmtLegGrp)?.Reset();
			FinancialStatus = null;
			CorporateAction = null;
			NetChgPrevDay = null;
			ApplQueueDepth = null;
			ApplQueueResolution = null;
			((IFixReset?)StandardTrailer)?.Reset();
			MDStreamID = null;
		}
	}
}
