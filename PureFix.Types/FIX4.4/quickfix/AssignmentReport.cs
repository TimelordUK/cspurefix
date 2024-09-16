using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AW", FixVersion.FIX44)]
	public sealed partial class AssignmentReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 833, Type = TagType.String, Offset = 1, Required = true)]
		public string? AsgnRptID { get; set; }
		
		[TagDetails(Tag = 832, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TotNumAssignmentReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? LastRptRequested { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 6, Required = true)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 10, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 11, Required = true)]
		public PositionQty? PositionQty { get; set; }
		
		[Component(Offset = 12, Required = true)]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(Tag = 834, Type = TagType.Float, Offset = 13, Required = false)]
		public double? ThresholdAmount { get; set; }
		
		[TagDetails(Tag = 730, Type = TagType.Float, Offset = 14, Required = true)]
		public double? SettlPrice { get; set; }
		
		[TagDetails(Tag = 731, Type = TagType.Int, Offset = 15, Required = true)]
		public int? SettlPriceType { get; set; }
		
		[TagDetails(Tag = 732, Type = TagType.Float, Offset = 16, Required = true)]
		public double? UnderlyingSettlPrice { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 744, Type = TagType.String, Offset = 18, Required = true)]
		public string? AssignmentMethod { get; set; }
		
		[TagDetails(Tag = 745, Type = TagType.Float, Offset = 19, Required = false)]
		public double? AssignmentUnit { get; set; }
		
		[TagDetails(Tag = 746, Type = TagType.Float, Offset = 20, Required = true)]
		public double? OpenInterest { get; set; }
		
		[TagDetails(Tag = 747, Type = TagType.String, Offset = 21, Required = true)]
		public string? ExerciseMethod { get; set; }
		
		[TagDetails(Tag = 716, Type = TagType.String, Offset = 22, Required = true)]
		public string? SettlSessID { get; set; }
		
		[TagDetails(Tag = 717, Type = TagType.String, Offset = 23, Required = true)]
		public string? SettlSessSubID { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 24, Required = true)]
		public DateOnly? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 25, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 26, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 27, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 28, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& AsgnRptID is not null
				&& Parties is not null && ((IFixValidator)Parties).IsValid(in config)
				&& AccountType is not null
				&& PositionQty is not null && ((IFixValidator)PositionQty).IsValid(in config)
				&& PositionAmountData is not null && ((IFixValidator)PositionAmountData).IsValid(in config)
				&& SettlPrice is not null
				&& SettlPriceType is not null
				&& UnderlyingSettlPrice is not null
				&& AssignmentMethod is not null
				&& OpenInterest is not null
				&& ExerciseMethod is not null
				&& SettlSessID is not null
				&& SettlSessSubID is not null
				&& ClearingBusinessDate is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (AsgnRptID is not null) writer.WriteString(833, AsgnRptID);
			if (TotNumAssignmentReports is not null) writer.WriteWholeNumber(832, TotNumAssignmentReports.Value);
			if (LastRptRequested is not null) writer.WriteBoolean(912, LastRptRequested.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (PositionQty is not null) ((IFixEncoder)PositionQty).Encode(writer);
			if (PositionAmountData is not null) ((IFixEncoder)PositionAmountData).Encode(writer);
			if (ThresholdAmount is not null) writer.WriteNumber(834, ThresholdAmount.Value);
			if (SettlPrice is not null) writer.WriteNumber(730, SettlPrice.Value);
			if (SettlPriceType is not null) writer.WriteWholeNumber(731, SettlPriceType.Value);
			if (UnderlyingSettlPrice is not null) writer.WriteNumber(732, UnderlyingSettlPrice.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (AssignmentMethod is not null) writer.WriteString(744, AssignmentMethod);
			if (AssignmentUnit is not null) writer.WriteNumber(745, AssignmentUnit.Value);
			if (OpenInterest is not null) writer.WriteNumber(746, OpenInterest.Value);
			if (ExerciseMethod is not null) writer.WriteString(747, ExerciseMethod);
			if (SettlSessID is not null) writer.WriteString(716, SettlSessID);
			if (SettlSessSubID is not null) writer.WriteString(717, SettlSessSubID);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
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
