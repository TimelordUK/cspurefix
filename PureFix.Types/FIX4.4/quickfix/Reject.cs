using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("3", FixVersion.FIX44)]
	public sealed partial class Reject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 45, Type = TagType.Int, Offset = 1, Required = true)]
		public int? RefSeqNum { get; set; }
		
		[TagDetails(Tag = 371, Type = TagType.Int, Offset = 2, Required = false)]
		public int? RefTagID { get; set; }
		
		[TagDetails(Tag = 372, Type = TagType.String, Offset = 3, Required = false)]
		public string? RefMsgType { get; set; }
		
		[TagDetails(Tag = 373, Type = TagType.Int, Offset = 4, Required = false)]
		public int? SessionRejectReason { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& RefSeqNum is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (RefSeqNum is not null) writer.WriteWholeNumber(45, RefSeqNum.Value);
			if (RefTagID is not null) writer.WriteWholeNumber(371, RefTagID.Value);
			if (RefMsgType is not null) writer.WriteString(372, RefMsgType);
			if (SessionRejectReason is not null) writer.WriteWholeNumber(373, SessionRejectReason.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
