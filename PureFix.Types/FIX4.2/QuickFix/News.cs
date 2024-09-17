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
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? OrigTime {get; set;}
		
		[TagDetails(Tag = 61, Type = TagType.String, Offset = 2, Required = false)]
		public string? Urgency {get; set;}
		
		[TagDetails(Tag = 148, Type = TagType.String, Offset = 3, Required = true)]
		public string? Headline {get; set;}
		
		[TagDetails(Tag = 358, Type = TagType.Length, Offset = 4, Required = false, LinksToTag = 359)]
		public int? EncodedHeadlineLen {get; set;}
		
		[TagDetails(Tag = 359, Type = TagType.RawData, Offset = 5, Required = false, LinksToTag = 358)]
		public byte[]? EncodedHeadline {get; set;}
		
		[Group(NoOfTag = 215, Offset = 6, Required = false)]
		public NoRoutingIDs[]? NoRoutingIDs {get; set;}
		
		[Group(NoOfTag = 146, Offset = 7, Required = false)]
		public NoRelatedSym[]? NoRelatedSym {get; set;}
		
		[Group(NoOfTag = 33, Offset = 8, Required = true)]
		public LinesOfText[]? LinesOfText {get; set;}
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 9, Required = false)]
		public string? URLLink {get; set;}
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 10, Required = false, LinksToTag = 96)]
		public int? RawDataLength {get; set;}
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 11, Required = false, LinksToTag = 95)]
		public byte[]? RawData {get; set;}
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
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
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			OrigTime = view.GetDateTime(42);
			Urgency = view.GetString(61);
			Headline = view.GetString(148);
			EncodedHeadlineLen = view.GetInt32(358);
			EncodedHeadline = view.GetByteArray(359);
			if (view.GetView("NoRoutingIDs") is IMessageView viewNoRoutingIDs)
			{
				var count = viewNoRoutingIDs.GroupCount();
				NoRoutingIDs = new NoRoutingIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRoutingIDs[i] = new();
					((IFixParser)NoRoutingIDs[i]).Parse(viewNoRoutingIDs.GetGroupInstance(i));
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
			if (view.GetView("LinesOfText") is IMessageView viewLinesOfText)
			{
				var count = viewLinesOfText.GroupCount();
				LinesOfText = new LinesOfText[count];
				for (int i = 0; i < count; i++)
				{
					LinesOfText[i] = new();
					((IFixParser)LinesOfText[i]).Parse(viewLinesOfText.GetGroupInstance(i));
				}
			}
			URLLink = view.GetString(149);
			RawDataLength = view.GetInt32(95);
			RawData = view.GetByteArray(96);
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
				case "OrigTime":
					value = OrigTime;
					break;
				case "Urgency":
					value = Urgency;
					break;
				case "Headline":
					value = Headline;
					break;
				case "EncodedHeadlineLen":
					value = EncodedHeadlineLen;
					break;
				case "EncodedHeadline":
					value = EncodedHeadline;
					break;
				case "NoRoutingIDs":
					value = NoRoutingIDs;
					break;
				case "NoRelatedSym":
					value = NoRelatedSym;
					break;
				case "LinesOfText":
					value = LinesOfText;
					break;
				case "URLLink":
					value = URLLink;
					break;
				case "RawDataLength":
					value = RawDataLength;
					break;
				case "RawData":
					value = RawData;
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
