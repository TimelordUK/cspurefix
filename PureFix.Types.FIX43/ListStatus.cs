using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("N", FixVersion.FIX43)]
	public sealed partial class ListStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
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
		
		[TagDetails(Tag = 445, Type = TagType.Length, Offset = 7, Required = false)]
		public int? EncodedListStatusTextLen {get; set;}
		
		[TagDetails(Tag = 446, Type = TagType.RawData, Offset = 8, Required = false)]
		public byte[]? EncodedListStatusText {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 68, Type = TagType.Int, Offset = 10, Required = true)]
		public int? TotNoOrders {get; set;}
		
		public sealed partial class NoOrders : IFixGroup
		{
			[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
			public string? ClOrdID {get; set;}
			
			[TagDetails(Tag = 526, Type = TagType.String, Offset = 1, Required = false)]
			public string? SecondaryClOrdID {get; set;}
			
			[TagDetails(Tag = 14, Type = TagType.Float, Offset = 2, Required = true)]
			public double? CumQty {get; set;}
			
			[TagDetails(Tag = 39, Type = TagType.String, Offset = 3, Required = true)]
			public string? OrdStatus {get; set;}
			
			[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 4, Required = false)]
			public bool? WorkingIndicator {get; set;}
			
			[TagDetails(Tag = 151, Type = TagType.Float, Offset = 5, Required = true)]
			public double? LeavesQty {get; set;}
			
			[TagDetails(Tag = 84, Type = TagType.Float, Offset = 6, Required = true)]
			public double? CxlQty {get; set;}
			
			[TagDetails(Tag = 6, Type = TagType.Float, Offset = 7, Required = true)]
			public double? AvgPx {get; set;}
			
			[TagDetails(Tag = 103, Type = TagType.Int, Offset = 8, Required = false)]
			public int? OrdRejReason {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 9, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 10, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 11, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
				if (CumQty is not null) writer.WriteNumber(14, CumQty.Value);
				if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
				if (WorkingIndicator is not null) writer.WriteBoolean(636, WorkingIndicator.Value);
				if (LeavesQty is not null) writer.WriteNumber(151, LeavesQty.Value);
				if (CxlQty is not null) writer.WriteNumber(84, CxlQty.Value);
				if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
				if (OrdRejReason is not null) writer.WriteWholeNumber(103, OrdRejReason.Value);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				ClOrdID = view.GetString(11);
				SecondaryClOrdID = view.GetString(526);
				CumQty = view.GetDouble(14);
				OrdStatus = view.GetString(39);
				WorkingIndicator = view.GetBool(636);
				LeavesQty = view.GetDouble(151);
				CxlQty = view.GetDouble(84);
				AvgPx = view.GetDouble(6);
				OrdRejReason = view.GetInt32(103);
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "ClOrdID":
					{
						value = ClOrdID;
						break;
					}
					case "SecondaryClOrdID":
					{
						value = SecondaryClOrdID;
						break;
					}
					case "CumQty":
					{
						value = CumQty;
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
					case "LeavesQty":
					{
						value = LeavesQty;
						break;
					}
					case "CxlQty":
					{
						value = CxlQty;
						break;
					}
					case "AvgPx":
					{
						value = AvgPx;
						break;
					}
					case "OrdRejReason":
					{
						value = OrdRejReason;
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
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				ClOrdID = null;
				SecondaryClOrdID = null;
				CumQty = null;
				OrdStatus = null;
				WorkingIndicator = null;
				LeavesQty = null;
				CxlQty = null;
				AvgPx = null;
				OrdRejReason = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 73, Offset = 11, Required = true)]
		public NoOrders[]? Orders {get; set;}
		
		[Component(Offset = 12, Required = true)]
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
			if (ListID is not null) writer.WriteString(66, ListID);
			if (ListStatusType is not null) writer.WriteWholeNumber(429, ListStatusType.Value);
			if (NoRpts is not null) writer.WriteWholeNumber(82, NoRpts.Value);
			if (ListOrderStatus is not null) writer.WriteWholeNumber(431, ListOrderStatus.Value);
			if (RptSeq is not null) writer.WriteWholeNumber(83, RptSeq.Value);
			if (ListStatusText is not null) writer.WriteString(444, ListStatusText);
			if (EncodedListStatusTextLen is not null) writer.WriteWholeNumber(445, EncodedListStatusTextLen.Value);
			if (EncodedListStatusText is not null) writer.WriteBuffer(446, EncodedListStatusText);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TotNoOrders is not null) writer.WriteWholeNumber(68, TotNoOrders.Value);
			if (Orders is not null && Orders.Length != 0)
			{
				writer.WriteWholeNumber(73, Orders.Length);
				for (int i = 0; i < Orders.Length; i++)
				{
					((IFixEncoder)Orders[i]).Encode(writer);
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
				Orders = new NoOrders[count];
				for (int i = 0; i < count; i++)
				{
					Orders[i] = new();
					((IFixParser)Orders[i]).Parse(viewNoOrders.GetGroupInstance(i));
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
				{
					value = StandardHeader;
					break;
				}
				case "ListID":
				{
					value = ListID;
					break;
				}
				case "ListStatusType":
				{
					value = ListStatusType;
					break;
				}
				case "NoRpts":
				{
					value = NoRpts;
					break;
				}
				case "ListOrderStatus":
				{
					value = ListOrderStatus;
					break;
				}
				case "RptSeq":
				{
					value = RptSeq;
					break;
				}
				case "ListStatusText":
				{
					value = ListStatusText;
					break;
				}
				case "EncodedListStatusTextLen":
				{
					value = EncodedListStatusTextLen;
					break;
				}
				case "EncodedListStatusText":
				{
					value = EncodedListStatusText;
					break;
				}
				case "TransactTime":
				{
					value = TransactTime;
					break;
				}
				case "TotNoOrders":
				{
					value = TotNoOrders;
					break;
				}
				case "NoOrders":
				{
					value = Orders;
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
			Orders = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
