using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("b", FixVersion.FIX42)]
	public sealed partial class QuoteAcknowledgement : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = false)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 297, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteAckStatus { get; set; }
		
		[TagDetails(Tag = 300, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRejectReason { get; set; }
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteResponseLevel { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text { get; set; }
		
		[Group(NoOfTag = 296, Offset = 8, Required = false)]
		public QuoteAcknowledgementNoQuoteSets[]? NoQuoteSets { get; set; }
		
		[Component(Offset = 9, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteAckStatus is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteAckStatus is not null) writer.WriteWholeNumber(297, QuoteAckStatus.Value);
			if (QuoteRejectReason is not null) writer.WriteWholeNumber(300, QuoteRejectReason.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (Text is not null) writer.WriteString(58, Text);
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
