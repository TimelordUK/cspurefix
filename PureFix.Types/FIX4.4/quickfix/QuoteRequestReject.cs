using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AG", FixVersion.FIX44)]
	public sealed partial class QuoteRequestReject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = true)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 2, Required = false)]
		public string? RFQReqID { get; set; }
		
		[TagDetails(Tag = 658, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteRequestRejectReason { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public QuotReqRjctGrp? QuotReqRjctGrp { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteReqID is not null
				&& QuoteRequestRejectReason is not null
				&& QuotReqRjctGrp is not null && ((IFixValidator)QuotReqRjctGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (RFQReqID is not null) writer.WriteString(644, RFQReqID);
			if (QuoteRequestRejectReason is not null) writer.WriteWholeNumber(658, QuoteRequestRejectReason.Value);
			if (QuotReqRjctGrp is not null) ((IFixEncoder)QuotReqRjctGrp).Encode(writer);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
