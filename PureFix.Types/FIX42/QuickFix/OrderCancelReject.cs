using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("9", FixVersion.FIX42)]
	public sealed partial class OrderCancelReject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 4, Required = true)]
		public string? OrigClOrdID {get; set;}
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 5, Required = true)]
		public string? OrdStatus {get; set;}
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 6, Required = false)]
		public string? ClientID {get; set;}
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 7, Required = false)]
		public string? ExecBroker {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 8, Required = false)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 9, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 434, Type = TagType.String, Offset = 11, Required = true)]
		public string? CxlRejResponseTo {get; set;}
		
		[TagDetails(Tag = 102, Type = TagType.Int, Offset = 12, Required = false)]
		public int? CxlRejReason {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 13, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 14, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 15, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 16, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& OrderID is not null
				&& ClOrdID is not null
				&& OrigClOrdID is not null
				&& OrdStatus is not null
				&& CxlRejResponseTo is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (Account is not null) writer.WriteString(1, Account);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (CxlRejResponseTo is not null) writer.WriteString(434, CxlRejResponseTo);
			if (CxlRejReason is not null) writer.WriteWholeNumber(102, CxlRejReason.Value);
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
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			ClOrdID = view.GetString(11);
			OrigClOrdID = view.GetString(41);
			OrdStatus = view.GetString(39);
			ClientID = view.GetString(109);
			ExecBroker = view.GetString(76);
			ListID = view.GetString(66);
			Account = view.GetString(1);
			TransactTime = view.GetDateTime(60);
			CxlRejResponseTo = view.GetString(434);
			CxlRejReason = view.GetInt32(102);
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
				case "OrderID":
					value = OrderID;
					break;
				case "SecondaryOrderID":
					value = SecondaryOrderID;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "OrigClOrdID":
					value = OrigClOrdID;
					break;
				case "OrdStatus":
					value = OrdStatus;
					break;
				case "ClientID":
					value = ClientID;
					break;
				case "ExecBroker":
					value = ExecBroker;
					break;
				case "ListID":
					value = ListID;
					break;
				case "Account":
					value = Account;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "CxlRejResponseTo":
					value = CxlRejResponseTo;
					break;
				case "CxlRejReason":
					value = CxlRejReason;
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
			OrderID = null;
			SecondaryOrderID = null;
			ClOrdID = null;
			OrigClOrdID = null;
			OrdStatus = null;
			ClientID = null;
			ExecBroker = null;
			ListID = null;
			Account = null;
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
