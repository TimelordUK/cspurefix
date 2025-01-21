using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("AllocationReportAck", FixVersion.FIX50SP2)]
	public sealed partial class AllocationReportAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 755, Type = TagType.String, Offset = 1, Required = true)]
		public string? AllocReportID {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 2, Required = false)]
		public string? AllocID {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 3, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 819, Type = TagType.Int, Offset = 4, Required = false)]
		public int? AvgPxIndicator {get; set;}
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 5, Required = false)]
		public double? Quantity {get; set;}
		
		[TagDetails(Tag = 71, Type = TagType.String, Offset = 6, Required = false)]
		public string? AllocTransType {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 7, Required = false)]
		public AllocationReportAckParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecondaryAllocID {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 9, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 87, Type = TagType.Int, Offset = 11, Required = false)]
		public int? AllocStatus {get; set;}
		
		[TagDetails(Tag = 88, Type = TagType.Int, Offset = 12, Required = false)]
		public int? AllocRejCode {get; set;}
		
		[TagDetails(Tag = 794, Type = TagType.Int, Offset = 13, Required = false)]
		public int? AllocReportType {get; set;}
		
		[TagDetails(Tag = 808, Type = TagType.Int, Offset = 14, Required = false)]
		public int? AllocIntermedReqType {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 15, Required = false)]
		public string? MatchStatus {get; set;}
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 16, Required = false)]
		public int? Product {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 17, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 18, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 19, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 20, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 21, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& AllocReportID is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (AllocReportID is not null) writer.WriteString(755, AllocReportID);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (AvgPxIndicator is not null) writer.WriteWholeNumber(819, AvgPxIndicator.Value);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (AllocTransType is not null) writer.WriteString(71, AllocTransType);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (SecondaryAllocID is not null) writer.WriteString(793, SecondaryAllocID);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (AllocStatus is not null) writer.WriteWholeNumber(87, AllocStatus.Value);
			if (AllocRejCode is not null) writer.WriteWholeNumber(88, AllocRejCode.Value);
			if (AllocReportType is not null) writer.WriteWholeNumber(794, AllocReportType.Value);
			if (AllocIntermedReqType is not null) writer.WriteWholeNumber(808, AllocIntermedReqType.Value);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
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
			AllocReportID = view.GetString(755);
			AllocID = view.GetString(70);
			ClearingBusinessDate = view.GetDateOnly(715);
			AvgPxIndicator = view.GetInt32(819);
			Quantity = view.GetDouble(53);
			AllocTransType = view.GetString(71);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new AllocationReportAckParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			SecondaryAllocID = view.GetString(793);
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			AllocStatus = view.GetInt32(87);
			AllocRejCode = view.GetInt32(88);
			AllocReportType = view.GetInt32(794);
			AllocIntermedReqType = view.GetInt32(808);
			MatchStatus = view.GetString(573);
			Product = view.GetInt32(460);
			SecurityType = view.GetString(167);
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
				case "AllocReportID":
					value = AllocReportID;
					break;
				case "AllocID":
					value = AllocID;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "AvgPxIndicator":
					value = AvgPxIndicator;
					break;
				case "Quantity":
					value = Quantity;
					break;
				case "AllocTransType":
					value = AllocTransType;
					break;
				case "Parties":
					value = Parties;
					break;
				case "SecondaryAllocID":
					value = SecondaryAllocID;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "AllocStatus":
					value = AllocStatus;
					break;
				case "AllocRejCode":
					value = AllocRejCode;
					break;
				case "AllocReportType":
					value = AllocReportType;
					break;
				case "AllocIntermedReqType":
					value = AllocIntermedReqType;
					break;
				case "MatchStatus":
					value = MatchStatus;
					break;
				case "Product":
					value = Product;
					break;
				case "SecurityType":
					value = SecurityType;
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
			AllocReportID = null;
			AllocID = null;
			ClearingBusinessDate = null;
			AvgPxIndicator = null;
			Quantity = null;
			AllocTransType = null;
			Parties = null;
			SecondaryAllocID = null;
			TradeDate = null;
			TransactTime = null;
			AllocStatus = null;
			AllocRejCode = null;
			AllocReportType = null;
			AllocIntermedReqType = null;
			MatchStatus = null;
			Product = null;
			SecurityType = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
