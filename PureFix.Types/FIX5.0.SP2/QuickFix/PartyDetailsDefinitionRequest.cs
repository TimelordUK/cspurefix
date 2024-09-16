using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix
{
	[MessageType("CX", FixVersion.FIX50SP2)]
	public sealed partial class PartyDetailsDefinitionRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 1505, Type = TagType.String, Offset = 1, Required = true)]
		public string? PartyDetailsListRequestID {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public RequestingPartyGrpComponent? RequestingPartyGrp {get; set;}
		
		[Component(Offset = 3, Required = true)]
		public PartyDetailsUpdateGrpComponent? PartyDetailsUpdateGrp {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 4, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 5, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 6, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 7, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PartyDetailsListRequestID is not null
				&& PartyDetailsUpdateGrp is not null && ((IFixValidator)PartyDetailsUpdateGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PartyDetailsListRequestID is not null) writer.WriteString(1505, PartyDetailsListRequestID);
			if (RequestingPartyGrp is not null) ((IFixEncoder)RequestingPartyGrp).Encode(writer);
			if (PartyDetailsUpdateGrp is not null) ((IFixEncoder)PartyDetailsUpdateGrp).Encode(writer);
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
			PartyDetailsListRequestID = view.GetString(1505);
			if (view.GetView("RequestingPartyGrp") is IMessageView viewRequestingPartyGrp)
			{
				RequestingPartyGrp = new();
				((IFixParser)RequestingPartyGrp).Parse(viewRequestingPartyGrp);
			}
			if (view.GetView("PartyDetailsUpdateGrp") is IMessageView viewPartyDetailsUpdateGrp)
			{
				PartyDetailsUpdateGrp = new();
				((IFixParser)PartyDetailsUpdateGrp).Parse(viewPartyDetailsUpdateGrp);
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
				case "PartyDetailsListRequestID":
					value = PartyDetailsListRequestID;
					break;
				case "RequestingPartyGrp":
					value = RequestingPartyGrp;
					break;
				case "PartyDetailsUpdateGrp":
					value = PartyDetailsUpdateGrp;
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
	}
}
