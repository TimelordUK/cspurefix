using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CrossOrderCancelRequestSideCrossOrdCxlGrpNoSides : IFixGroup
	{
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrigClOrdID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 2, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 4, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[TagDetails(Tag = 586, Type = TagType.UtcTimestamp, Offset = 5, Required = false)]
		public DateTime? OrigOrdModTime {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 7, Required = false)]
		public DateOnly? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 8, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[Component(Offset = 9, Required = true)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 10, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Side is not null
				&& OrigClOrdID is not null
				&& ClOrdID is not null
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Side is not null) writer.WriteString(54, Side);
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (OrigOrdModTime is not null) writer.WriteUtcTimeStamp(586, OrigOrdModTime.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Side = view.GetString(54);
			OrigClOrdID = view.GetString(41);
			ClOrdID = view.GetString(11);
			SecondaryClOrdID = view.GetString(526);
			ClOrdLinkID = view.GetString(583);
			OrigOrdModTime = view.GetDateTime(586);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			TradeOriginationDate = view.GetDateOnly(229);
			TradeDate = view.GetDateOnly(75);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			ComplianceID = view.GetString(376);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Side":
					value = Side;
					break;
				case "OrigClOrdID":
					value = OrigClOrdID;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "ClOrdLinkID":
					value = ClOrdLinkID;
					break;
				case "OrigOrdModTime":
					value = OrigOrdModTime;
					break;
				case "Parties":
					value = Parties;
					break;
				case "TradeOriginationDate":
					value = TradeOriginationDate;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "OrderQtyData":
					value = OrderQtyData;
					break;
				case "ComplianceID":
					value = ComplianceID;
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
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Side = null;
			OrigClOrdID = null;
			ClOrdID = null;
			SecondaryClOrdID = null;
			ClOrdLinkID = null;
			OrigOrdModTime = null;
			((IFixReset?)Parties)?.Reset();
			TradeOriginationDate = null;
			TradeDate = null;
			((IFixReset?)OrderQtyData)?.Reset();
			ComplianceID = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
		}
	}
}
