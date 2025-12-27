using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("9", FixVersion.FIX44)]
	public sealed partial class OrderCancelReject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 4, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 5, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 6, Required = true)]
		public string? OrigClOrdID {get; set;}
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 7, Required = true)]
		public string? OrdStatus {get; set;}
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? WorkingIndicator {get; set;}
		
		[TagDetails(Tag = 586, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? OrigOrdModTime {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 10, Required = false)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 11, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 12, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 13, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 14, Required = false)]
		public DateOnly? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 15, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 16, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 434, Type = TagType.String, Offset = 17, Required = true)]
		public string? CxlRejResponseTo {get; set;}
		
		[TagDetails(Tag = 102, Type = TagType.Int, Offset = 18, Required = false)]
		public int? CxlRejReason {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 19, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 20, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 21, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 22, Required = true)]
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
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (WorkingIndicator is not null) writer.WriteBoolean(636, WorkingIndicator.Value);
			if (OrigOrdModTime is not null) writer.WriteUtcTimeStamp(586, OrigOrdModTime.Value);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (CxlRejResponseTo is not null) writer.WriteString(434, CxlRejResponseTo);
			if (CxlRejReason is not null) writer.WriteWholeNumber(102, CxlRejReason.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			SecondaryClOrdID = view.GetString(526);
			ClOrdID = view.GetString(11);
			ClOrdLinkID = view.GetString(583);
			OrigClOrdID = view.GetString(41);
			OrdStatus = view.GetString(39);
			WorkingIndicator = view.GetBool(636);
			OrigOrdModTime = view.GetDateTime(586);
			ListID = view.GetString(66);
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			TradeOriginationDate = view.GetDateOnly(229);
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			CxlRejResponseTo = view.GetString(434);
			CxlRejReason = view.GetInt32(102);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
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
				case "OrderID":
				{
					value = OrderID;
					break;
				}
				case "SecondaryOrderID":
				{
					value = SecondaryOrderID;
					break;
				}
				case "SecondaryClOrdID":
				{
					value = SecondaryClOrdID;
					break;
				}
				case "ClOrdID":
				{
					value = ClOrdID;
					break;
				}
				case "ClOrdLinkID":
				{
					value = ClOrdLinkID;
					break;
				}
				case "OrigClOrdID":
				{
					value = OrigClOrdID;
					break;
				}
				case "OrdStatus":
				{
					value = OrdStatus;
					break;
				}
				case "WorkingIndicator":
				{
					value = WorkingIndicator;
					break;
				}
				case "OrigOrdModTime":
				{
					value = OrigOrdModTime;
					break;
				}
				case "ListID":
				{
					value = ListID;
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
				case "TradeOriginationDate":
				{
					value = TradeOriginationDate;
					break;
				}
				case "TradeDate":
				{
					value = TradeDate;
					break;
				}
				case "TransactTime":
				{
					value = TransactTime;
					break;
				}
				case "CxlRejResponseTo":
				{
					value = CxlRejResponseTo;
					break;
				}
				case "CxlRejReason":
				{
					value = CxlRejReason;
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
			OrderID = null;
			SecondaryOrderID = null;
			SecondaryClOrdID = null;
			ClOrdID = null;
			ClOrdLinkID = null;
			OrigClOrdID = null;
			OrdStatus = null;
			WorkingIndicator = null;
			OrigOrdModTime = null;
			ListID = null;
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			TradeOriginationDate = null;
			TradeDate = null;
			TransactTime = null;
			CxlRejResponseTo = null;
			CxlRejReason = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
