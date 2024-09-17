using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix
{
	[MessageType("CT", FixVersion.FIX50SP2)]
	public sealed partial class PartyRiskLimitsDefinitionRequestAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 1666, Type = TagType.String, Offset = 1, Required = true)]
		public string? RiskLimitRequestID {get; set;}
		
		[TagDetails(Tag = 1761, Type = TagType.Int, Offset = 2, Required = false)]
		public int? RiskLimitRequestResult {get; set;}
		
		[TagDetails(Tag = 1762, Type = TagType.Int, Offset = 3, Required = true)]
		public int? RiskLimitRequestStatus {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public RequestingPartyGrpComponent? RequestingPartyGrp {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public PartyRiskLimitsAckGrpComponent? PartyRiskLimitsAckGrp {get; set;}
		
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
				&& RiskLimitRequestID is not null
				&& RiskLimitRequestStatus is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (RiskLimitRequestID is not null) writer.WriteString(1666, RiskLimitRequestID);
			if (RiskLimitRequestResult is not null) writer.WriteWholeNumber(1761, RiskLimitRequestResult.Value);
			if (RiskLimitRequestStatus is not null) writer.WriteWholeNumber(1762, RiskLimitRequestStatus.Value);
			if (RequestingPartyGrp is not null) ((IFixEncoder)RequestingPartyGrp).Encode(writer);
			if (PartyRiskLimitsAckGrp is not null) ((IFixEncoder)PartyRiskLimitsAckGrp).Encode(writer);
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
			RiskLimitRequestID = view.GetString(1666);
			RiskLimitRequestResult = view.GetInt32(1761);
			RiskLimitRequestStatus = view.GetInt32(1762);
			if (view.GetView("RequestingPartyGrp") is IMessageView viewRequestingPartyGrp)
			{
				RequestingPartyGrp = new();
				((IFixParser)RequestingPartyGrp).Parse(viewRequestingPartyGrp);
			}
			if (view.GetView("PartyRiskLimitsAckGrp") is IMessageView viewPartyRiskLimitsAckGrp)
			{
				PartyRiskLimitsAckGrp = new();
				((IFixParser)PartyRiskLimitsAckGrp).Parse(viewPartyRiskLimitsAckGrp);
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
				case "RiskLimitRequestID":
					value = RiskLimitRequestID;
					break;
				case "RiskLimitRequestResult":
					value = RiskLimitRequestResult;
					break;
				case "RiskLimitRequestStatus":
					value = RiskLimitRequestStatus;
					break;
				case "RequestingPartyGrp":
					value = RequestingPartyGrp;
					break;
				case "PartyRiskLimitsAckGrp":
					value = PartyRiskLimitsAckGrp;
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
