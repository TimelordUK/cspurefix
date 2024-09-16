using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("r", FixVersion.FIX43)]
	public sealed partial class OrderMassCancelReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 3, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 530, Type = TagType.String, Offset = 5, Required = true)]
		public string? MassCancelRequestType { get; set; }
		
		[TagDetails(Tag = 531, Type = TagType.String, Offset = 6, Required = true)]
		public string? MassCancelResponse { get; set; }
		
		[TagDetails(Tag = 532, Type = TagType.String, Offset = 7, Required = false)]
		public string? MassCancelRejectReason { get; set; }
		
		[TagDetails(Tag = 533, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TotalAffectedOrders { get; set; }
		
		[Group(NoOfTag = 534, Offset = 9, Required = false)]
		public OrderMassCancelReportNoAffectedOrders[]? NoAffectedOrders { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 10, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 14, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 15, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 16, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 17, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 18, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 19, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& OrderID is not null
				&& MassCancelRequestType is not null
				&& MassCancelResponse is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (MassCancelRequestType is not null) writer.WriteString(530, MassCancelRequestType);
			if (MassCancelResponse is not null) writer.WriteString(531, MassCancelResponse);
			if (MassCancelRejectReason is not null) writer.WriteString(532, MassCancelRejectReason);
			if (TotalAffectedOrders is not null) writer.WriteWholeNumber(533, TotalAffectedOrders.Value);
			if (NoAffectedOrders is not null && NoAffectedOrders.Length != 0)
			{
				writer.WriteWholeNumber(534, NoAffectedOrders.Length);
				for (int i = 0; i < NoAffectedOrders.Length; i++)
				{
					((IFixEncoder)NoAffectedOrders[i]).Encode(writer);
				}
			}
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
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
