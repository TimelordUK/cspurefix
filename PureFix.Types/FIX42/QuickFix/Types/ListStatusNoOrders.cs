using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class ListStatusNoOrders : IFixGroup
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 1, Required = true)]
		public double? CumQty {get; set;}
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 2, Required = true)]
		public string? OrdStatus {get; set;}
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 3, Required = true)]
		public double? LeavesQty {get; set;}
		
		[TagDetails(Tag = 84, Type = TagType.Float, Offset = 4, Required = true)]
		public double? CxlQty {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 5, Required = true)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 6, Required = false)]
		public int? OrdRejReason {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				ClOrdID is not null
				&& CumQty is not null
				&& OrdStatus is not null
				&& LeavesQty is not null
				&& CxlQty is not null
				&& AvgPx is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (CumQty is not null) writer.WriteNumber(14, CumQty.Value);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (LeavesQty is not null) writer.WriteNumber(151, LeavesQty.Value);
			if (CxlQty is not null) writer.WriteNumber(84, CxlQty.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (OrdRejReason is not null) writer.WriteWholeNumber(103, OrdRejReason.Value);
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
			
			ClOrdID = view.GetString(11);
			CumQty = view.GetDouble(14);
			OrdStatus = view.GetString(39);
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
					value = ClOrdID;
					break;
				case "CumQty":
					value = CumQty;
					break;
				case "OrdStatus":
					value = OrdStatus;
					break;
				case "LeavesQty":
					value = LeavesQty;
					break;
				case "CxlQty":
					value = CxlQty;
					break;
				case "AvgPx":
					value = AvgPx;
					break;
				case "OrdRejReason":
					value = OrdRejReason;
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
			ClOrdID = null;
			CumQty = null;
			OrdStatus = null;
			LeavesQty = null;
			CxlQty = null;
			AvgPx = null;
			OrdRejReason = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
		}
	}
}
