using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("N", FixVersion.FIX43)]
	public sealed partial class ListStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 429, Type = TagType.Int, Offset = 2, Required = true)]
		public int? ListStatusType {get; set;}
		
		[TagDetails(Tag = 82, Type = TagType.Int, Offset = 3, Required = true)]
		public int? NoRpts {get; set;}
		
		[TagDetails(Tag = 431, Type = TagType.Int, Offset = 4, Required = true)]
		public int? ListOrderStatus {get; set;}
		
		[TagDetails(Tag = 83, Type = TagType.Int, Offset = 5, Required = true)]
		public int? RptSeq {get; set;}
		
		[TagDetails(Tag = 444, Type = TagType.String, Offset = 6, Required = false)]
		public string? ListStatusText {get; set;}
		
		[TagDetails(Tag = 445, Type = TagType.Length, Offset = 7, Required = false, LinksToTag = 446)]
		public int? EncodedListStatusTextLen {get; set;}
		
		[TagDetails(Tag = 446, Type = TagType.RawData, Offset = 8, Required = false, LinksToTag = 445)]
		public byte[]? EncodedListStatusText {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 68, Type = TagType.Int, Offset = 10, Required = true)]
		public int? TotNoOrders {get; set;}
		
		[Group(NoOfTag = 73, Offset = 11, Required = true)]
		public ListStatusNoOrders[]? NoOrders {get; set;}
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ListID is not null
				&& ListStatusType is not null
				&& NoRpts is not null
				&& ListOrderStatus is not null
				&& RptSeq is not null
				&& TotNoOrders is not null
				&& NoOrders is not null && FixValidator.IsValid(NoOrders, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (ListStatusType is not null) writer.WriteWholeNumber(429, ListStatusType.Value);
			if (NoRpts is not null) writer.WriteWholeNumber(82, NoRpts.Value);
			if (ListOrderStatus is not null) writer.WriteWholeNumber(431, ListOrderStatus.Value);
			if (RptSeq is not null) writer.WriteWholeNumber(83, RptSeq.Value);
			if (ListStatusText is not null) writer.WriteString(444, ListStatusText);
			if (EncodedListStatusText is not null)
			{
				writer.WriteWholeNumber(445, EncodedListStatusText.Length);
				writer.WriteBuffer(446, EncodedListStatusText);
			}
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
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
			ListStatusType = view.GetInt32(429);
			NoRpts = view.GetInt32(82);
			ListOrderStatus = view.GetInt32(431);
			RptSeq = view.GetInt32(83);
			ListStatusText = view.GetString(444);
			EncodedListStatusTextLen = view.GetInt32(445);
			EncodedListStatusText = view.GetByteArray(446);
			TransactTime = view.GetDateTime(60);
			TotNoOrders = view.GetInt32(68);
			if (view.GetView("NoOrders") is IMessageView viewNoOrders)
			{
				var count = viewNoOrders.GroupCount();
				NoOrders = new ListStatusNoOrders[count];
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
				case "ListStatusType":
					value = ListStatusType;
					break;
				case "NoRpts":
					value = NoRpts;
					break;
				case "ListOrderStatus":
					value = ListOrderStatus;
					break;
				case "RptSeq":
					value = RptSeq;
					break;
				case "ListStatusText":
					value = ListStatusText;
					break;
				case "EncodedListStatusTextLen":
					value = EncodedListStatusTextLen;
					break;
				case "EncodedListStatusText":
					value = EncodedListStatusText;
					break;
				case "TransactTime":
					value = TransactTime;
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
			ListStatusType = null;
			NoRpts = null;
			ListOrderStatus = null;
			RptSeq = null;
			ListStatusText = null;
			EncodedListStatusTextLen = null;
			EncodedListStatusText = null;
			TransactTime = null;
			TotNoOrders = null;
			NoOrders = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
