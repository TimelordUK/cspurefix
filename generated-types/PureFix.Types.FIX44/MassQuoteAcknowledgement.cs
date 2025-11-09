using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("b", FixVersion.FIX44)]
	public sealed partial class MassQuoteAcknowledgement : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 297, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteStatus {get; set;}
		
		[TagDetails(Tag = 300, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRejectReason {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 6, Required = false)]
		public int? QuoteType {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public Parties? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public QuotSetAckGrp? QuotSetAckGrp {get; set;}
		
		[Component(Offset = 15, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteStatus is not null) writer.WriteWholeNumber(297, QuoteStatus.Value);
			if (QuoteRejectReason is not null) writer.WriteWholeNumber(300, QuoteRejectReason.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (QuotSetAckGrp is not null) ((IFixEncoder)QuotSetAckGrp).Encode(writer);
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
			QuoteID = view.GetString(117);
			QuoteStatus = view.GetInt32(297);
			QuoteRejectReason = view.GetInt32(300);
			QuoteResponseLevel = view.GetInt32(301);
			QuoteType = view.GetInt32(537);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("QuotSetAckGrp") is IMessageView viewQuotSetAckGrp)
			{
				QuotSetAckGrp = new();
				((IFixParser)QuotSetAckGrp).Parse(viewQuotSetAckGrp);
			}
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
				{
					value = StandardHeader;
					break;
				}
				case "QuoteReqID":
				{
					value = QuoteReqID;
					break;
				}
				case "QuoteID":
				{
					value = QuoteID;
					break;
				}
				case "QuoteStatus":
				{
					value = QuoteStatus;
					break;
				}
				case "QuoteRejectReason":
				{
					value = QuoteRejectReason;
					break;
				}
				case "QuoteResponseLevel":
				{
					value = QuoteResponseLevel;
					break;
				}
				case "QuoteType":
				{
					value = QuoteType;
					break;
				}
				case "Parties":
				{
					value = Parties;
					break;
				}
				case "Account":
				{
					value = Account;
					break;
				}
				case "AcctIDSource":
				{
					value = AcctIDSource;
					break;
				}
				case "AccountType":
				{
					value = AccountType;
					break;
				}
				case "Text":
				{
					value = Text;
					break;
				}
				case "EncodedTextLen":
				{
					value = EncodedTextLen;
					break;
				}
				case "EncodedText":
				{
					value = EncodedText;
					break;
				}
				case "QuotSetAckGrp":
				{
					value = QuotSetAckGrp;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			QuoteReqID = null;
			QuoteID = null;
			QuoteStatus = null;
			QuoteRejectReason = null;
			QuoteResponseLevel = null;
			QuoteType = null;
			((IFixReset?)Parties)?.Reset();
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)QuotSetAckGrp)?.Reset();
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
