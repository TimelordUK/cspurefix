using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AZ", FixVersion.FIX44)]
	public sealed partial class CollateralResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 904, Type = TagType.String, Offset = 1, Required = true)]
		public string? CollRespID { get; set; }
		
		[TagDetails(Tag = 902, Type = TagType.String, Offset = 2, Required = true)]
		public string? CollAsgnID { get; set; }
		
		[TagDetails(Tag = 894, Type = TagType.String, Offset = 3, Required = false)]
		public string? CollReqID { get; set; }
		
		[TagDetails(Tag = 895, Type = TagType.Int, Offset = 4, Required = true)]
		public int? CollAsgnReason { get; set; }
		
		[TagDetails(Tag = 903, Type = TagType.Int, Offset = 5, Required = false)]
		public int? CollAsgnTransType { get; set; }
		
		[TagDetails(Tag = 905, Type = TagType.Int, Offset = 6, Required = true)]
		public int? CollAsgnRespType { get; set; }
		
		[TagDetails(Tag = 906, Type = TagType.Int, Offset = 7, Required = false)]
		public int? CollAsgnRejectReason { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 8, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 10, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 11, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 12, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 13, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 14, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[Component(Offset = 16, Required = false)]
		public ExecCollGrp? ExecCollGrp { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public TrdCollGrp? TrdCollGrp { get; set; }
		
		[Component(Offset = 18, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 19, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 20, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 21, Required = false)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 22, Required = false)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 23, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 24, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 25, Required = false)]
		public UndInstrmtCollGrp? UndInstrmtCollGrp { get; set; }
		
		[TagDetails(Tag = 899, Type = TagType.Float, Offset = 26, Required = false)]
		public double? MarginExcess { get; set; }
		
		[TagDetails(Tag = 900, Type = TagType.Float, Offset = 27, Required = false)]
		public double? TotalNetValue { get; set; }
		
		[TagDetails(Tag = 901, Type = TagType.Float, Offset = 28, Required = false)]
		public double? CashOutstanding { get; set; }
		
		[Component(Offset = 29, Required = false)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30, Required = false)]
		public string? Side { get; set; }
		
		[Component(Offset = 31, Required = false)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 32, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 33, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 34, Required = false)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 35, Required = false)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 36, Required = false)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 37, Required = false)]
		public double? EndCash { get; set; }
		
		[Component(Offset = 38, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 39, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 40, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 41, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 42, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 43, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& CollRespID is not null
				&& CollAsgnID is not null
				&& CollAsgnReason is not null
				&& CollAsgnRespType is not null
				&& TransactTime is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (CollRespID is not null) writer.WriteString(904, CollRespID);
			if (CollAsgnID is not null) writer.WriteString(902, CollAsgnID);
			if (CollReqID is not null) writer.WriteString(894, CollReqID);
			if (CollAsgnReason is not null) writer.WriteWholeNumber(895, CollAsgnReason.Value);
			if (CollAsgnTransType is not null) writer.WriteWholeNumber(903, CollAsgnTransType.Value);
			if (CollAsgnRespType is not null) writer.WriteWholeNumber(905, CollAsgnRespType.Value);
			if (CollAsgnRejectReason is not null) writer.WriteWholeNumber(906, CollAsgnRejectReason.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ExecCollGrp is not null) ((IFixEncoder)ExecCollGrp).Encode(writer);
			if (TrdCollGrp is not null) ((IFixEncoder)TrdCollGrp).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtCollGrp is not null) ((IFixEncoder)UndInstrmtCollGrp).Encode(writer);
			if (MarginExcess is not null) writer.WriteNumber(899, MarginExcess.Value);
			if (TotalNetValue is not null) writer.WriteNumber(900, TotalNetValue.Value);
			if (CashOutstanding is not null) writer.WriteNumber(901, CashOutstanding.Value);
			if (TrdRegTimestamps is not null) ((IFixEncoder)TrdRegTimestamps).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (MiscFeesGrp is not null) ((IFixEncoder)MiscFeesGrp).Encode(writer);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
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
