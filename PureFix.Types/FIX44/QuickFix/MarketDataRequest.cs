using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("V", FixVersion.FIX44)]
	public sealed partial class MarketDataRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = true)]
		public string? MDReqID {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 2, Required = true)]
		public string? SubscriptionRequestType {get; set;}
		
		[TagDetails(Tag = 264, Type = TagType.Int, Offset = 3, Required = true)]
		public int? MarketDepth {get; set;}
		
		[TagDetails(Tag = 265, Type = TagType.Int, Offset = 4, Required = false)]
		public int? MDUpdateType {get; set;}
		
		[TagDetails(Tag = 266, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? AggregatedBook {get; set;}
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 6, Required = false)]
		public string? OpenCloseSettlFlag {get; set;}
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 7, Required = false)]
		public string? Scope {get; set;}
		
		[TagDetails(Tag = 547, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? MDImplicitDelete {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public MDReqGrpComponent? MDReqGrp {get; set;}
		
		[Component(Offset = 10, Required = true)]
		public InstrmtMDReqGrpComponent? InstrmtMDReqGrp {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public TrdgSesGrpComponent? TrdgSesGrp {get; set;}
		
		[TagDetails(Tag = 815, Type = TagType.Int, Offset = 12, Required = false)]
		public int? ApplQueueAction {get; set;}
		
		[TagDetails(Tag = 812, Type = TagType.Int, Offset = 13, Required = false)]
		public int? ApplQueueMax {get; set;}
		
		[Component(Offset = 14, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& MDReqID is not null
				&& SubscriptionRequestType is not null
				&& MarketDepth is not null
				&& MDReqGrp is not null && ((IFixValidator)MDReqGrp).IsValid(in config)
				&& InstrmtMDReqGrp is not null && ((IFixValidator)InstrmtMDReqGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (MarketDepth is not null) writer.WriteWholeNumber(264, MarketDepth.Value);
			if (MDUpdateType is not null) writer.WriteWholeNumber(265, MDUpdateType.Value);
			if (AggregatedBook is not null) writer.WriteBoolean(266, AggregatedBook.Value);
			if (OpenCloseSettlFlag is not null) writer.WriteString(286, OpenCloseSettlFlag);
			if (Scope is not null) writer.WriteString(546, Scope);
			if (MDImplicitDelete is not null) writer.WriteBoolean(547, MDImplicitDelete.Value);
			if (MDReqGrp is not null) ((IFixEncoder)MDReqGrp).Encode(writer);
			if (InstrmtMDReqGrp is not null) ((IFixEncoder)InstrmtMDReqGrp).Encode(writer);
			if (TrdgSesGrp is not null) ((IFixEncoder)TrdgSesGrp).Encode(writer);
			if (ApplQueueAction is not null) writer.WriteWholeNumber(815, ApplQueueAction.Value);
			if (ApplQueueMax is not null) writer.WriteWholeNumber(812, ApplQueueMax.Value);
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
			SubscriptionRequestType = view.GetString(263);
			MarketDepth = view.GetInt32(264);
			MDUpdateType = view.GetInt32(265);
			AggregatedBook = view.GetBool(266);
			OpenCloseSettlFlag = view.GetString(286);
			Scope = view.GetString(546);
			MDImplicitDelete = view.GetBool(547);
			if (view.GetView("MDReqGrp") is IMessageView viewMDReqGrp)
			{
				MDReqGrp = new();
				((IFixParser)MDReqGrp).Parse(viewMDReqGrp);
			}
			if (view.GetView("InstrmtMDReqGrp") is IMessageView viewInstrmtMDReqGrp)
			{
				InstrmtMDReqGrp = new();
				((IFixParser)InstrmtMDReqGrp).Parse(viewInstrmtMDReqGrp);
			}
			if (view.GetView("TrdgSesGrp") is IMessageView viewTrdgSesGrp)
			{
				TrdgSesGrp = new();
				((IFixParser)TrdgSesGrp).Parse(viewTrdgSesGrp);
			}
			ApplQueueAction = view.GetInt32(815);
			ApplQueueMax = view.GetInt32(812);
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
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
					break;
				case "MarketDepth":
					value = MarketDepth;
					break;
				case "MDUpdateType":
					value = MDUpdateType;
					break;
				case "AggregatedBook":
					value = AggregatedBook;
					break;
				case "OpenCloseSettlFlag":
					value = OpenCloseSettlFlag;
					break;
				case "Scope":
					value = Scope;
					break;
				case "MDImplicitDelete":
					value = MDImplicitDelete;
					break;
				case "MDReqGrp":
					value = MDReqGrp;
					break;
				case "InstrmtMDReqGrp":
					value = InstrmtMDReqGrp;
					break;
				case "TrdgSesGrp":
					value = TrdgSesGrp;
					break;
				case "ApplQueueAction":
					value = ApplQueueAction;
					break;
				case "ApplQueueMax":
					value = ApplQueueMax;
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
			SubscriptionRequestType = null;
			MarketDepth = null;
			MDUpdateType = null;
			AggregatedBook = null;
			OpenCloseSettlFlag = null;
			Scope = null;
			MDImplicitDelete = null;
			((IFixReset?)MDReqGrp)?.Reset();
			((IFixReset?)InstrmtMDReqGrp)?.Reset();
			((IFixReset?)TrdgSesGrp)?.Reset();
			ApplQueueAction = null;
			ApplQueueMax = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
