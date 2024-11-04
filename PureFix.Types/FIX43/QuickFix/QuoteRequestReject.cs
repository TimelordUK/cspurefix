using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AG", FixVersion.FIX43)]
	public sealed partial class QuoteRequestReject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = true)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 2, Required = false)]
		public string? RFQReqID {get; set;}
		
		[TagDetails(Tag = 658, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteRequestRejectReason {get; set;}
		
		[Group(NoOfTag = 146, Offset = 4, Required = true)]
		public NoRelatedSym[]? NoRelatedSym {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteReqID is not null
				&& QuoteRequestRejectReason is not null
				&& NoRelatedSym is not null && FixValidator.IsValid(NoRelatedSym, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (RFQReqID is not null) writer.WriteString(644, RFQReqID);
			if (QuoteRequestRejectReason is not null) writer.WriteWholeNumber(658, QuoteRequestRejectReason.Value);
			if (NoRelatedSym is not null && NoRelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, NoRelatedSym.Length);
				for (int i = 0; i < NoRelatedSym.Length; i++)
				{
					((IFixEncoder)NoRelatedSym[i]).Encode(writer);
				}
			}
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
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
			RFQReqID = view.GetString(644);
			QuoteRequestRejectReason = view.GetInt32(658);
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
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
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
				case "RFQReqID":
					value = RFQReqID;
					break;
				case "QuoteRequestRejectReason":
					value = QuoteRequestRejectReason;
					break;
				case "NoRelatedSym":
					value = NoRelatedSym;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
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
			QuoteReqID = null;
			RFQReqID = null;
			QuoteRequestRejectReason = null;
			NoRelatedSym = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
