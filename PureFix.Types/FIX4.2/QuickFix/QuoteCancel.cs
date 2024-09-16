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
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = true)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 298, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteCancelType { get; set; }
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteResponseLevel { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[Group(NoOfTag = 295, Offset = 6, Required = true)]
		public QuoteCancelNoQuoteEntries[]? NoQuoteEntries { get; set; }
		
		[Component(Offset = 7, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
