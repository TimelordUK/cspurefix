using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("i", FixVersion.FIX42)]
	public sealed partial class MassQuote : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = true)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 3, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[TagDetails(Tag = 293, Type = TagType.Float, Offset = 4, Required = false)]
		public double? DefBidSize {get; set;}
		
		[TagDetails(Tag = 294, Type = TagType.Float, Offset = 5, Required = false)]
		public double? DefOfferSize {get; set;}
		
		[Group(NoOfTag = 296, Offset = 6, Required = true)]
		public NoQuoteSets[]? NoQuoteSets {get; set;}
		
		[Component(Offset = 7, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteID is not null
				&& NoQuoteSets is not null && FixValidator.IsValid(NoQuoteSets, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (DefBidSize is not null) writer.WriteNumber(293, DefBidSize.Value);
			if (DefOfferSize is not null) writer.WriteNumber(294, DefOfferSize.Value);
			if (NoQuoteSets is not null && NoQuoteSets.Length != 0)
			{
				writer.WriteWholeNumber(296, NoQuoteSets.Length);
				for (int i = 0; i < NoQuoteSets.Length; i++)
				{
					((IFixEncoder)NoQuoteSets[i]).Encode(writer);
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
			QuoteResponseLevel = view.GetInt32(301);
			DefBidSize = view.GetDouble(293);
			DefOfferSize = view.GetDouble(294);
			if (view.GetView("NoQuoteSets") is IMessageView viewNoQuoteSets)
			{
				var count = viewNoQuoteSets.GroupCount();
				NoQuoteSets = new NoQuoteSets[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteSets[i] = new();
					((IFixParser)NoQuoteSets[i]).Parse(viewNoQuoteSets.GetGroupInstance(i));
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
				case "QuoteResponseLevel":
					value = QuoteResponseLevel;
					break;
				case "DefBidSize":
					value = DefBidSize;
					break;
				case "DefOfferSize":
					value = DefOfferSize;
					break;
				case "NoQuoteSets":
					value = NoQuoteSets;
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
