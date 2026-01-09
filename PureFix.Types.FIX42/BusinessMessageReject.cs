using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("j", FixVersion.FIX42)]
	public sealed partial class BusinessMessageReject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 45, Type = TagType.Int, Offset = 1, Required = false)]
		public int? RefSeqNum {get; set;}
		
		[TagDetails(Tag = 372, Type = TagType.String, Offset = 2, Required = true)]
		public string? RefMsgType {get; set;}
		
		[TagDetails(Tag = 379, Type = TagType.String, Offset = 3, Required = false)]
		public string? BusinessRejectRefID {get; set;}
		
		[TagDetails(Tag = 380, Type = TagType.Int, Offset = 4, Required = true)]
		public int? BusinessRejectReason {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 8, Required = true)]
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
			if (RefSeqNum is not null) writer.WriteWholeNumber(45, RefSeqNum.Value);
			if (RefMsgType is not null) writer.WriteString(372, RefMsgType);
			if (BusinessRejectRefID is not null) writer.WriteString(379, BusinessRejectRefID);
			if (BusinessRejectReason is not null) writer.WriteWholeNumber(380, BusinessRejectReason.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RefSeqNum = view.GetInt32(45);
			RefMsgType = view.GetString(372);
			BusinessRejectRefID = view.GetString(379);
			BusinessRejectReason = view.GetInt32(380);
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
				case "RefSeqNum":
				{
					value = RefSeqNum;
					break;
				}
				case "RefMsgType":
				{
					value = RefMsgType;
					break;
				}
				case "BusinessRejectRefID":
				{
					value = BusinessRejectRefID;
					break;
				}
				case "BusinessRejectReason":
				{
					value = BusinessRejectReason;
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
			RefSeqNum = null;
			RefMsgType = null;
			BusinessRejectRefID = null;
			BusinessRejectReason = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
