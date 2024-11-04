using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix
{
	[MessageType("DB", FixVersion.FIX50SP2)]
	public sealed partial class PartyEntitlementsDefinitionRequestAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 1770, Type = TagType.String, Offset = 1, Required = true)]
		public string? EntitlementRequestID {get; set;}
		
		[TagDetails(Tag = 1882, Type = TagType.Int, Offset = 2, Required = true)]
		public int? EntitlementRequestStatus {get; set;}
		
		[TagDetails(Tag = 1881, Type = TagType.Int, Offset = 3, Required = false)]
		public int? EntitlementRequestResult {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public RequestingPartyGrpComponent? RequestingPartyGrp {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public PartyEntitlementAckGrpComponent? PartyEntitlementAckGrp {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 6, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 7, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 8, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& EntitlementRequestID is not null
				&& EntitlementRequestStatus is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EntitlementRequestID is not null) writer.WriteString(1770, EntitlementRequestID);
			if (EntitlementRequestStatus is not null) writer.WriteWholeNumber(1882, EntitlementRequestStatus.Value);
			if (EntitlementRequestResult is not null) writer.WriteWholeNumber(1881, EntitlementRequestResult.Value);
			if (RequestingPartyGrp is not null) ((IFixEncoder)RequestingPartyGrp).Encode(writer);
			if (PartyEntitlementAckGrp is not null) ((IFixEncoder)PartyEntitlementAckGrp).Encode(writer);
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
			EntitlementRequestStatus = view.GetInt32(1882);
			EntitlementRequestResult = view.GetInt32(1881);
			if (view.GetView("RequestingPartyGrp") is IMessageView viewRequestingPartyGrp)
			{
				RequestingPartyGrp = new();
				((IFixParser)RequestingPartyGrp).Parse(viewRequestingPartyGrp);
			}
			if (view.GetView("PartyEntitlementAckGrp") is IMessageView viewPartyEntitlementAckGrp)
			{
				PartyEntitlementAckGrp = new();
				((IFixParser)PartyEntitlementAckGrp).Parse(viewPartyEntitlementAckGrp);
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
				case "EntitlementRequestStatus":
					value = EntitlementRequestStatus;
					break;
				case "EntitlementRequestResult":
					value = EntitlementRequestResult;
					break;
				case "RequestingPartyGrp":
					value = RequestingPartyGrp;
					break;
				case "PartyEntitlementAckGrp":
					value = PartyEntitlementAckGrp;
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
			EntitlementRequestID = null;
			EntitlementRequestStatus = null;
			EntitlementRequestResult = null;
			((IFixReset?)RequestingPartyGrp)?.Reset();
			((IFixReset?)PartyEntitlementAckGrp)?.Reset();
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
