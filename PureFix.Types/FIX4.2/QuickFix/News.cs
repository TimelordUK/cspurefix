using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("B", FixVersion.FIX42)]
	public sealed partial class News : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(Tag = 61, Type = TagType.String, Offset = 2, Required = false)]
		public string? Urgency { get; set; }
		
		[TagDetails(Tag = 148, Type = TagType.String, Offset = 3, Required = true)]
		public string? Headline { get; set; }
		
		[TagDetails(Tag = 358, Type = TagType.Length, Offset = 4, Required = false, LinksToTag = 359)]
		public int? EncodedHeadlineLen { get; set; }
		
		[TagDetails(Tag = 359, Type = TagType.RawData, Offset = 5, Required = false, LinksToTag = 358)]
		public byte[]? EncodedHeadline { get; set; }
		
		[Group(NoOfTag = 215, Offset = 6, Required = false)]
		public NewsNoRoutingIDs[]? NoRoutingIDs { get; set; }
		
		[Group(NoOfTag = 146, Offset = 7, Required = false)]
		public NewsNoRelatedSym[]? NoRelatedSym { get; set; }
		
		[Group(NoOfTag = 33, Offset = 8, Required = true)]
		public NewsLinesOfText[]? LinesOfText { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 9, Required = false)]
		public string? URLLink { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 10, Required = false, LinksToTag = 96)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 11, Required = false, LinksToTag = 95)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Headline is not null
				&& LinesOfText is not null && FixValidator.IsValid(LinesOfText, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrigTime is not null) writer.WriteUtcTimeStamp(42, OrigTime.Value);
			if (Urgency is not null) writer.WriteString(61, Urgency);
			if (Headline is not null) writer.WriteString(148, Headline);
			if (EncodedHeadline is not null)
			{
				writer.WriteWholeNumber(358, EncodedHeadline.Length);
				writer.WriteBuffer(359, EncodedHeadline);
			}
			if (NoRoutingIDs is not null && NoRoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, NoRoutingIDs.Length);
				for (int i = 0; i < NoRoutingIDs.Length; i++)
				{
					((IFixEncoder)NoRoutingIDs[i]).Encode(writer);
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
			if (LinesOfText is not null && LinesOfText.Length != 0)
			{
				writer.WriteWholeNumber(33, LinesOfText.Length);
				for (int i = 0; i < LinesOfText.Length; i++)
				{
					((IFixEncoder)LinesOfText[i]).Encode(writer);
				}
			}
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (RawData is not null)
			{
				writer.WriteWholeNumber(95, RawData.Length);
				writer.WriteBuffer(96, RawData);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
