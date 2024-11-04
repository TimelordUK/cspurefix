using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("V", FixVersion.FIX42)]
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
		
		[Group(NoOfTag = 267, Offset = 6, Required = true)]
		public NoMDEntryTypes[]? NoMDEntryTypes {get; set;}
		
		[Group(NoOfTag = 146, Offset = 7, Required = true)]
		public NoRelatedSym[]? NoRelatedSym {get; set;}
		
		[Component(Offset = 8, Required = true)]
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
				case "NoMDEntryTypes":
					value = NoMDEntryTypes;
					break;
				case "NoRelatedSym":
					value = NoRelatedSym;
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
			NoMDEntryTypes = null;
			NoRelatedSym = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
