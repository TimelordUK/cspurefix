using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("MarketDataIncrementalRefresh", FixVersion.FIX50SP2)]
	public sealed partial class MarketDataIncrementalRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 1021, Type = TagType.Int, Offset = 2, Required = false)]
		public int? MDBookType {get; set;}
		
		[TagDetails(Tag = 1022, Type = TagType.String, Offset = 3, Required = false)]
		public string? MDFeedType {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 5, Required = false)]
		public string? MDReqID {get; set;}
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 6, Required = false)]
		public int? ApplQueueDepth {get; set;}
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 7, Required = false)]
		public int? ApplQueueResolution {get; set;}
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
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
			if (MDBookType is not null) writer.WriteWholeNumber(1021, MDBookType.Value);
			if (MDFeedType is not null) writer.WriteString(1022, MDFeedType);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
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
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			MDBookType = view.GetInt32(1021);
			MDFeedType = view.GetString(1022);
			TradeDate = view.GetDateOnly(75);
			MDReqID = view.GetString(262);
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
				case "ApplicationSequenceControl":
					value = ApplicationSequenceControl;
					break;
				case "MDBookType":
					value = MDBookType;
					break;
				case "MDFeedType":
					value = MDFeedType;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "MDReqID":
					value = MDReqID;
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
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			MDBookType = null;
			MDFeedType = null;
			TradeDate = null;
			MDReqID = null;
			ApplQueueDepth = null;
			ApplQueueResolution = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
