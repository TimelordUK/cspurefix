using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("Z", FixVersion.FIX42)]
	public sealed partial class QuoteCancel : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = true)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 298, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteCancelType {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[Group(NoOfTag = 295, Offset = 6, Required = true)]
		public NoQuoteEntries[]? NoQuoteEntries {get; set;}
		
		[Component(Offset = 7, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteID is not null
				&& QuoteCancelType is not null
				&& NoQuoteEntries is not null && FixValidator.IsValid(NoQuoteEntries, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteCancelType is not null) writer.WriteWholeNumber(298, QuoteCancelType.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (NoQuoteEntries is not null && NoQuoteEntries.Length != 0)
			{
				writer.WriteWholeNumber(295, NoQuoteEntries.Length);
				for (int i = 0; i < NoQuoteEntries.Length; i++)
				{
					((IFixEncoder)NoQuoteEntries[i]).Encode(writer);
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
			QuoteReqID = view.GetString(131);
			QuoteID = view.GetString(117);
			QuoteCancelType = view.GetInt32(298);
			QuoteResponseLevel = view.GetInt32(301);
			TradingSessionID = view.GetString(336);
			if (view.GetView("NoQuoteEntries") is IMessageView viewNoQuoteEntries)
			{
				var count = viewNoQuoteEntries.GroupCount();
				NoQuoteEntries = new NoQuoteEntries[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteEntries[i] = new();
					((IFixParser)NoQuoteEntries[i]).Parse(viewNoQuoteEntries.GetGroupInstance(i));
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
				case "QuoteReqID":
					value = QuoteReqID;
					break;
				case "QuoteID":
					value = QuoteID;
					break;
				case "QuoteCancelType":
					value = QuoteCancelType;
					break;
				case "QuoteResponseLevel":
					value = QuoteResponseLevel;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "NoQuoteEntries":
					value = NoQuoteEntries;
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
