using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("ApplicationMessageRequestAck", FixVersion.FIX50SP2)]
	public sealed partial class ApplicationMessageRequestAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 1353, Type = TagType.String, Offset = 1, Required = true)]
		public string? ApplResponseID {get; set;}
		
		[TagDetails(Tag = 1346, Type = TagType.String, Offset = 2, Required = false)]
		public string? ApplReqID {get; set;}
		
		[TagDetails(Tag = 1347, Type = TagType.Int, Offset = 3, Required = false)]
		public int? ApplReqType {get; set;}
		
		[TagDetails(Tag = 1348, Type = TagType.Int, Offset = 4, Required = false)]
		public int? ApplResponseType {get; set;}
		
		[TagDetails(Tag = 1349, Type = TagType.Int, Offset = 5, Required = false)]
		public int? ApplTotalMessageCount {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 6, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 7, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 8, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[Group(NoOfTag = 1012, Offset = 10, Required = false)]
		public ApplicationMessageRequestAckParties[]? Parties {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ApplResponseID is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplResponseID is not null) writer.WriteString(1353, ApplResponseID);
			if (ApplReqID is not null) writer.WriteString(1346, ApplReqID);
			if (ApplReqType is not null) writer.WriteWholeNumber(1347, ApplReqType.Value);
			if (ApplResponseType is not null) writer.WriteWholeNumber(1348, ApplResponseType.Value);
			if (ApplTotalMessageCount is not null) writer.WriteWholeNumber(1349, ApplTotalMessageCount.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			ApplResponseID = view.GetString(1353);
			ApplReqID = view.GetString(1346);
			ApplReqType = view.GetInt32(1347);
			ApplResponseType = view.GetInt32(1348);
			ApplTotalMessageCount = view.GetInt32(1349);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new ApplicationMessageRequestAckParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
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
				case "ApplResponseID":
					value = ApplResponseID;
					break;
				case "ApplReqID":
					value = ApplReqID;
					break;
				case "ApplReqType":
					value = ApplReqType;
					break;
				case "ApplResponseType":
					value = ApplResponseType;
					break;
				case "ApplTotalMessageCount":
					value = ApplTotalMessageCount;
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
				case "Parties":
					value = Parties;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			ApplResponseID = null;
			ApplReqID = null;
			ApplReqType = null;
			ApplResponseType = null;
			ApplTotalMessageCount = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
			Parties = null;
		}
	}
}
