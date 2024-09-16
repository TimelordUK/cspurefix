using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("C", FixVersion.FIX43)]
	public sealed partial class Email : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 164, Type = TagType.String, Offset = 1, Required = true)]
		public string? EmailThreadID { get; set; }
		
		[TagDetails(Tag = 94, Type = TagType.String, Offset = 2, Required = true)]
		public string? EmailType { get; set; }
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 3, Required = false)]
		public DateTime? OrigTime { get; set; }
		
		[TagDetails(Tag = 147, Type = TagType.String, Offset = 4, Required = true)]
		public string? Subject { get; set; }
		
		[TagDetails(Tag = 356, Type = TagType.Length, Offset = 5, Required = false, LinksToTag = 357)]
		public int? EncodedSubjectLen { get; set; }
		
		[TagDetails(Tag = 357, Type = TagType.RawData, Offset = 6, Required = false, LinksToTag = 356)]
		public byte[]? EncodedSubject { get; set; }
		
		[Group(NoOfTag = 215, Offset = 7, Required = false)]
		public EmailNoRoutingIDs[]? NoRoutingIDs { get; set; }
		
		[Group(NoOfTag = 146, Offset = 8, Required = false)]
		public EmailNoRelatedSym[]? NoRelatedSym { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 9, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 10, Required = false)]
		public string? ClOrdID { get; set; }
		
		[Group(NoOfTag = 33, Offset = 11, Required = true)]
		public EmailLinesOfText[]? LinesOfText { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 96)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 95)]
		public byte[]? RawData { get; set; }
		
		[Component(Offset = 14, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& EmailThreadID is not null
				&& EmailType is not null
				&& Subject is not null
				&& LinesOfText is not null && FixValidator.IsValid(LinesOfText, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EmailThreadID is not null) writer.WriteString(164, EmailThreadID);
			if (EmailType is not null) writer.WriteString(94, EmailType);
			if (OrigTime is not null) writer.WriteUtcTimeStamp(42, OrigTime.Value);
			if (Subject is not null) writer.WriteString(147, Subject);
			if (EncodedSubject is not null)
			{
				writer.WriteWholeNumber(356, EncodedSubject.Length);
				writer.WriteBuffer(357, EncodedSubject);
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
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (LinesOfText is not null && LinesOfText.Length != 0)
			{
				writer.WriteWholeNumber(33, LinesOfText.Length);
				for (int i = 0; i < LinesOfText.Length; i++)
				{
					((IFixEncoder)LinesOfText[i]).Encode(writer);
				}
			}
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
