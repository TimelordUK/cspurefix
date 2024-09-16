using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix
{
	[MessageType("DA", FixVersion.FIX50SP2)]
	public sealed partial class PartyEntitlementsDefinitionRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 1770, Type = TagType.String, Offset = 1, Required = true)]
		public string? EntitlementRequestID {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public RequestingPartyGrpComponent? RequestingPartyGrp {get; set;}
		
		[Component(Offset = 3, Required = true)]
		public PartyEntitlementUpdateGrpComponent? PartyEntitlementUpdateGrp {get; set;}
		
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
				&& EntitlementRequestID is not null
				&& PartyEntitlementUpdateGrp is not null && ((IFixValidator)PartyEntitlementUpdateGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EntitlementRequestID is not null) writer.WriteString(1770, EntitlementRequestID);
			if (RequestingPartyGrp is not null) ((IFixEncoder)RequestingPartyGrp).Encode(writer);
			if (PartyEntitlementUpdateGrp is not null) ((IFixEncoder)PartyEntitlementUpdateGrp).Encode(writer);
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
			EntitlementRequestID = view.GetString(1770);
			if (view.GetView("RequestingPartyGrp") is IMessageView viewRequestingPartyGrp)
			{
				RequestingPartyGrp = new();
				((IFixParser)RequestingPartyGrp).Parse(viewRequestingPartyGrp);
			}
			if (view.GetView("PartyEntitlementUpdateGrp") is IMessageView viewPartyEntitlementUpdateGrp)
			{
				PartyEntitlementUpdateGrp = new();
				((IFixParser)PartyEntitlementUpdateGrp).Parse(viewPartyEntitlementUpdateGrp);
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
				case "EntitlementRequestID":
					value = EntitlementRequestID;
					break;
				case "RequestingPartyGrp":
					value = RequestingPartyGrp;
					break;
				case "PartyEntitlementUpdateGrp":
					value = PartyEntitlementUpdateGrp;
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
