using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("Q", FixVersion.FIX44)]
	public sealed partial class DontKnowTrade : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 3, Required = true)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 127, Type = TagType.String, Offset = 4, Required = true)]
		public string? DKReason { get; set; }
		
		[Component(Offset = 5, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 8, Required = true)]
		public string? Side { get; set; }
		
		[Component(Offset = 9, Required = true)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 10, Required = false)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 11, Required = false)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 12, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 13, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 14, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 15, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& OrderID is not null
				&& ExecID is not null
				&& DKReason is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Side is not null
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (DKReason is not null) writer.WriteString(127, DKReason);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
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
