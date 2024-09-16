using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AO", FixVersion.FIX44)]
	public sealed partial class RequestForPositionsAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 721, Type = TagType.String, Offset = 1, Required = true)]
		public string? PosMaintRptID { get; set; }
		
		[TagDetails(Tag = 710, Type = TagType.String, Offset = 2, Required = false)]
		public string? PosReqID { get; set; }
		
		[TagDetails(Tag = 727, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TotalNumPosReports { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 728, Type = TagType.Int, Offset = 5, Required = true)]
		public int? PosReqResult { get; set; }
		
		[TagDetails(Tag = 729, Type = TagType.Int, Offset = 6, Required = true)]
		public int? PosReqStatus { get; set; }
		
		[Component(Offset = 7, Required = true)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = true)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10, Required = true)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 12, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 14, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 15, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 16, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 17, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 20, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& PosMaintRptID is not null
				&& PosReqResult is not null
				&& PosReqStatus is not null
				&& Parties is not null && ((IFixValidator)Parties).IsValid(in config)
				&& Account is not null
				&& AccountType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (PosMaintRptID is not null) writer.WriteString(721, PosMaintRptID);
			if (PosReqID is not null) writer.WriteString(710, PosReqID);
			if (TotalNumPosReports is not null) writer.WriteWholeNumber(727, TotalNumPosReports.Value);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (PosReqResult is not null) writer.WriteWholeNumber(728, PosReqResult.Value);
			if (PosReqStatus is not null) writer.WriteWholeNumber(729, PosReqStatus.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
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
