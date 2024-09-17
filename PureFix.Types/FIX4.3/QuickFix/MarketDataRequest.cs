using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("V", FixVersion.FIX43)]
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
		public string? OpenCloseSettleFlag {get; set;}
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 7, Required = false)]
		public string? Scope {get; set;}
		
		[TagDetails(Tag = 547, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? MDImplicitDelete {get; set;}
		
		[Group(NoOfTag = 267, Offset = 9, Required = true)]
		public NoMDEntryTypes[]? NoMDEntryTypes {get; set;}
		
		[Group(NoOfTag = 146, Offset = 10, Required = true)]
		public NoRelatedSym[]? NoRelatedSym {get; set;}
		
		[Group(NoOfTag = 386, Offset = 11, Required = false)]
		public NoTradingSessions[]? NoTradingSessions {get; set;}
		
		[Component(Offset = 12, Required = true)]
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
				&& NoMDEntryTypes is not null && FixValidator.IsValid(NoMDEntryTypes, in config)
				&& NoRelatedSym is not null && FixValidator.IsValid(NoRelatedSym, in config)
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
			if (OpenCloseSettleFlag is not null) writer.WriteString(286, OpenCloseSettleFlag);
			if (Scope is not null) writer.WriteString(546, Scope);
			if (MDImplicitDelete is not null) writer.WriteBoolean(547, MDImplicitDelete.Value);
			if (NoMDEntryTypes is not null && NoMDEntryTypes.Length != 0)
			{
				writer.WriteWholeNumber(267, NoMDEntryTypes.Length);
				for (int i = 0; i < NoMDEntryTypes.Length; i++)
				{
					((IFixEncoder)NoMDEntryTypes[i]).Encode(writer);
				}
			}
			if (NoRelatedSym is not null && NoRelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, NoRelatedSym.Length);
				for (int i = 0; i < NoRelatedSym.Length; i++)
				{
					((IFixEncoder)NoRelatedSym[i]).Encode(writer);
				}
			}
			if (NoTradingSessions is not null && NoTradingSessions.Length != 0)
			{
				writer.WriteWholeNumber(386, NoTradingSessions.Length);
				for (int i = 0; i < NoTradingSessions.Length; i++)
				{
					((IFixEncoder)NoTradingSessions[i]).Encode(writer);
				}
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
			MDReqID = view.GetString(262);
			SubscriptionRequestType = view.GetString(263);
			MarketDepth = view.GetInt32(264);
			MDUpdateType = view.GetInt32(265);
			AggregatedBook = view.GetBool(266);
			OpenCloseSettleFlag = view.GetString(286);
			Scope = view.GetString(546);
			MDImplicitDelete = view.GetBool(547);
			if (view.GetView("NoMDEntryTypes") is IMessageView viewNoMDEntryTypes)
			{
				var count = viewNoMDEntryTypes.GroupCount();
				NoMDEntryTypes = new NoMDEntryTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoMDEntryTypes[i] = new();
					((IFixParser)NoMDEntryTypes[i]).Parse(viewNoMDEntryTypes.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				NoRelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedSym[i] = new();
					((IFixParser)NoRelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoTradingSessions") is IMessageView viewNoTradingSessions)
			{
				var count = viewNoTradingSessions.GroupCount();
				NoTradingSessions = new NoTradingSessions[count];
				for (int i = 0; i < count; i++)
				{
					NoTradingSessions[i] = new();
					((IFixParser)NoTradingSessions[i]).Parse(viewNoTradingSessions.GetGroupInstance(i));
				}
			}
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
				case "OpenCloseSettleFlag":
					value = OpenCloseSettleFlag;
					break;
				case "Scope":
					value = Scope;
					break;
				case "MDImplicitDelete":
					value = MDImplicitDelete;
					break;
				case "NoMDEntryTypes":
					value = NoMDEntryTypes;
					break;
				case "NoRelatedSym":
					value = NoRelatedSym;
					break;
				case "NoTradingSessions":
					value = NoTradingSessions;
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
