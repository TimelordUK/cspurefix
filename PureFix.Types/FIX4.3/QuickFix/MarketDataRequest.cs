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
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = true)]
		public string? MDReqID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 2, Required = true)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 264, Type = TagType.Int, Offset = 3, Required = true)]
		public int? MarketDepth { get; set; }
		
		[TagDetails(Tag = 265, Type = TagType.Int, Offset = 4, Required = false)]
		public int? MDUpdateType { get; set; }
		
		[TagDetails(Tag = 266, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? AggregatedBook { get; set; }
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 6, Required = false)]
		public string? OpenCloseSettleFlag { get; set; }
		
		[TagDetails(Tag = 546, Type = TagType.String, Offset = 7, Required = false)]
		public string? Scope { get; set; }
		
		[TagDetails(Tag = 547, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? MDImplicitDelete { get; set; }
		
		[Group(NoOfTag = 267, Offset = 9, Required = true)]
		public MarketDataRequestNoMDEntryTypes[]? NoMDEntryTypes { get; set; }
		
		[Group(NoOfTag = 146, Offset = 10, Required = true)]
		public MarketDataRequestNoRelatedSym[]? NoRelatedSym { get; set; }
		
		[Group(NoOfTag = 386, Offset = 11, Required = false)]
		public MarketDataRequestNoTradingSessions[]? NoTradingSessions { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
