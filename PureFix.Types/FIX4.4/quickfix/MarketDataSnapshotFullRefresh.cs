using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("W", FixVersion.FIX44)]
	public sealed partial class MarketDataSnapshotFullRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID {get; set;}
		
		[Component(Offset = 2, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 5, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 6, Required = false)]
		public string? CorporateAction {get; set;}
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 7, Required = false)]
		public double? NetChgPrevDay {get; set;}
		
		[Component(Offset = 8, Required = true)]
		public MDFullGrpComponent? MDFullGrp {get; set;}
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 9, Required = false)]
		public int? ApplQueueDepth {get; set;}
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 10, Required = false)]
		public int? ApplQueueResolution {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& MDFullGrp is not null && ((IFixValidator)MDFullGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (NetChgPrevDay is not null) writer.WriteNumber(451, NetChgPrevDay.Value);
			if (MDFullGrp is not null) ((IFixEncoder)MDFullGrp).Encode(writer);
			if (ApplQueueDepth is not null) writer.WriteWholeNumber(813, ApplQueueDepth.Value);
			if (ApplQueueResolution is not null) writer.WriteWholeNumber(814, ApplQueueResolution.Value);
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
			if (view.GetView("MDFullGrp") is IMessageView viewMDFullGrp)
			{
				MDFullGrp = new();
				((IFixParser)MDFullGrp).Parse(viewMDFullGrp);
			}
			ApplQueueDepth = view.GetInt32(813);
			ApplQueueResolution = view.GetInt32(814);
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
				case "MDFullGrp":
					value = MDFullGrp;
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
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			MDReqID = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			((IFixReset?)InstrmtLegGrp)?.Reset();
			FinancialStatus = null;
			CorporateAction = null;
			NetChgPrevDay = null;
			((IFixReset?)MDFullGrp)?.Reset();
			ApplQueueDepth = null;
			ApplQueueResolution = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
