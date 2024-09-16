using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("d", FixVersion.FIX44)]
	public sealed partial class SecurityDefinition : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID { get; set; }
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID { get; set; }
		
		[TagDetails(Tag = 323, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityResponseType { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 7, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 10, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 11, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 12, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 14, Required = false)]
		public int? ExpirationCycle { get; set; }
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 15, Required = false)]
		public double? RoundLot { get; set; }
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 16, Required = false)]
		public double? MinTradeVol { get; set; }
		
		[Component(Offset = 17, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SecurityReqID is not null
				&& SecurityResponseID is not null
				&& SecurityResponseType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityResponseType is not null) writer.WriteWholeNumber(323, SecurityResponseType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (ExpirationCycle is not null) writer.WriteWholeNumber(827, ExpirationCycle.Value);
			if (RoundLot is not null) writer.WriteNumber(561, RoundLot.Value);
			if (MinTradeVol is not null) writer.WriteNumber(562, MinTradeVol.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
