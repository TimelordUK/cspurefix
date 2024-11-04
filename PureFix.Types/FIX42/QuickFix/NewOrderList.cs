using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("E", FixVersion.FIX42)]
	public sealed partial class NewOrderList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 2, Required = false)]
		public string? BidID {get; set;}
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClientBidID {get; set;}
		
		[TagDetails(Tag = 414, Type = TagType.Int, Offset = 4, Required = false)]
		public int? ProgRptReqs {get; set;}
		
		[TagDetails(Tag = 394, Type = TagType.Int, Offset = 5, Required = true)]
		public int? BidType {get; set;}
		
		[TagDetails(Tag = 415, Type = TagType.Int, Offset = 6, Required = false)]
		public int? ProgPeriodInterval {get; set;}
		
		[TagDetails(Tag = 433, Type = TagType.String, Offset = 7, Required = false)]
		public string? ListExecInstType {get; set;}
		
		[TagDetails(Tag = 69, Type = TagType.String, Offset = 8, Required = false)]
		public string? ListExecInst {get; set;}
		
		[TagDetails(Tag = 352, Type = TagType.Length, Offset = 9, Required = false, LinksToTag = 353)]
		public int? EncodedListExecInstLen {get; set;}
		
		[TagDetails(Tag = 353, Type = TagType.RawData, Offset = 10, Required = false, LinksToTag = 352)]
		public byte[]? EncodedListExecInst {get; set;}
		
		[TagDetails(Tag = 68, Type = TagType.Int, Offset = 11, Required = true)]
		public int? TotNoOrders {get; set;}
		
		[Group(NoOfTag = 73, Offset = 12, Required = true)]
		public NoOrders[]? NoOrders {get; set;}
		
		[Component(Offset = 13, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ListID is not null
				&& BidType is not null
				&& TotNoOrders is not null
				&& NoOrders is not null && FixValidator.IsValid(NoOrders, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (BidID is not null) writer.WriteString(390, BidID);
			if (ClientBidID is not null) writer.WriteString(391, ClientBidID);
			if (ProgRptReqs is not null) writer.WriteWholeNumber(414, ProgRptReqs.Value);
			if (BidType is not null) writer.WriteWholeNumber(394, BidType.Value);
			if (ProgPeriodInterval is not null) writer.WriteWholeNumber(415, ProgPeriodInterval.Value);
			if (ListExecInstType is not null) writer.WriteString(433, ListExecInstType);
			if (ListExecInst is not null) writer.WriteString(69, ListExecInst);
			if (EncodedListExecInst is not null)
			{
				writer.WriteWholeNumber(352, EncodedListExecInst.Length);
				writer.WriteBuffer(353, EncodedListExecInst);
			}
			if (TotNoOrders is not null) writer.WriteWholeNumber(68, TotNoOrders.Value);
			if (NoOrders is not null && NoOrders.Length != 0)
			{
				writer.WriteWholeNumber(73, NoOrders.Length);
				for (int i = 0; i < NoOrders.Length; i++)
				{
					((IFixEncoder)NoOrders[i]).Encode(writer);
				}
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
			ListID = view.GetString(66);
			BidID = view.GetString(390);
			ClientBidID = view.GetString(391);
			ProgRptReqs = view.GetInt32(414);
			BidType = view.GetInt32(394);
			ProgPeriodInterval = view.GetInt32(415);
			ListExecInstType = view.GetString(433);
			ListExecInst = view.GetString(69);
			EncodedListExecInstLen = view.GetInt32(352);
			EncodedListExecInst = view.GetByteArray(353);
			TotNoOrders = view.GetInt32(68);
			if (view.GetView("NoOrders") is IMessageView viewNoOrders)
			{
				var count = viewNoOrders.GroupCount();
				NoOrders = new NoOrders[count];
				for (int i = 0; i < count; i++)
				{
					NoOrders[i] = new();
					((IFixParser)NoOrders[i]).Parse(viewNoOrders.GetGroupInstance(i));
				}
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
					value = StandardHeader;
					break;
				case "ListID":
					value = ListID;
					break;
				case "BidID":
					value = BidID;
					break;
				case "ClientBidID":
					value = ClientBidID;
					break;
				case "ProgRptReqs":
					value = ProgRptReqs;
					break;
				case "BidType":
					value = BidType;
					break;
				case "ProgPeriodInterval":
					value = ProgPeriodInterval;
					break;
				case "ListExecInstType":
					value = ListExecInstType;
					break;
				case "ListExecInst":
					value = ListExecInst;
					break;
				case "EncodedListExecInstLen":
					value = EncodedListExecInstLen;
					break;
				case "EncodedListExecInst":
					value = EncodedListExecInst;
					break;
				case "TotNoOrders":
					value = TotNoOrders;
					break;
				case "NoOrders":
					value = NoOrders;
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
			ListID = null;
			BidID = null;
			ClientBidID = null;
			ProgRptReqs = null;
			BidType = null;
			ProgPeriodInterval = null;
			ListExecInstType = null;
			ListExecInst = null;
			EncodedListExecInstLen = null;
			EncodedListExecInst = null;
			TotNoOrders = null;
			NoOrders = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
