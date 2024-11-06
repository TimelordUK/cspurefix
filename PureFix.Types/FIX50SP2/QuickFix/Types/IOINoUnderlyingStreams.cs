using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingStreams : IFixGroup
	{
		[TagDetails(Tag = 40541, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingStreamType {get; set;}
		
		[TagDetails(Tag = 42016, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingStreamXID {get; set;}
		
		[TagDetails(Tag = 40542, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingStreamDesc {get; set;}
		
		[TagDetails(Tag = 43083, Type = TagType.String, Offset = 3, Required = false)]
		public string? UnderlyingStreamVersion {get; set;}
		
		[TagDetails(Tag = 43084, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? UnderlyingStreamVersionEffectiveDate {get; set;}
		
		[TagDetails(Tag = 40543, Type = TagType.Int, Offset = 5, Required = false)]
		public int? UnderlyingStreamPaySide {get; set;}
		
		[TagDetails(Tag = 40544, Type = TagType.Int, Offset = 6, Required = false)]
		public int? UnderlyingStreamReceiveSide {get; set;}
		
		[TagDetails(Tag = 42018, Type = TagType.String, Offset = 7, Required = false)]
		public string? UnderlyingStreamNotionalXIDRef {get; set;}
		
		[TagDetails(Tag = 40545, Type = TagType.Float, Offset = 8, Required = false)]
		public double? UnderlyingStreamNotional {get; set;}
		
		[TagDetails(Tag = 40546, Type = TagType.String, Offset = 9, Required = false)]
		public string? UnderlyingStreamCurrency {get; set;}
		
		[TagDetails(Tag = 43085, Type = TagType.String, Offset = 10, Required = false)]
		public string? UnderlyingStreamNotionalDeterminationMethod {get; set;}
		
		[TagDetails(Tag = 43086, Type = TagType.Int, Offset = 11, Required = false)]
		public int? UnderlyingStreamNotionalAdjustments {get; set;}
		
		[TagDetails(Tag = 42019, Type = TagType.Int, Offset = 12, Required = false)]
		public int? UnderlyingStreamNotionalFrequencyPeriod {get; set;}
		
		[TagDetails(Tag = 42020, Type = TagType.String, Offset = 13, Required = false)]
		public string? UnderlyingStreamNotionalFrequencyUnit {get; set;}
		
		[TagDetails(Tag = 42021, Type = TagType.Int, Offset = 14, Required = false)]
		public int? UnderlyingStreamNotionalCommodityFrequency {get; set;}
		
		[TagDetails(Tag = 42022, Type = TagType.String, Offset = 15, Required = false)]
		public string? UnderlyingStreamNotionalUnitOfMeasure {get; set;}
		
		[TagDetails(Tag = 42023, Type = TagType.Float, Offset = 16, Required = false)]
		public double? UnderlyingStreamTotalNotional {get; set;}
		
		[TagDetails(Tag = 42024, Type = TagType.String, Offset = 17, Required = false)]
		public string? UnderlyingStreamTotalNotionalUnitOfMeasure {get; set;}
		
		[Component(Offset = 18, Required = false)]
		public UnderlyingStreamCommodityComponent? UnderlyingStreamCommodity {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public UnderlyingStreamEffectiveDateComponent? UnderlyingStreamEffectiveDate {get; set;}
		
		[Component(Offset = 20, Required = false)]
		public UnderlyingStreamTerminationDateComponent? UnderlyingStreamTerminationDate {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public UnderlyingStreamCalculationPeriodDatesComponent? UnderlyingStreamCalculationPeriodDates {get; set;}
		
		[Component(Offset = 22, Required = false)]
		public UnderlyingPaymentStreamComponent? UnderlyingPaymentStream {get; set;}
		
		[Component(Offset = 23, Required = false)]
		public UnderlyingPaymentScheduleGrpComponent? UnderlyingPaymentScheduleGrp {get; set;}
		
		[Component(Offset = 24, Required = false)]
		public UnderlyingPaymentStubGrpComponent? UnderlyingPaymentStubGrp {get; set;}
		
		[Component(Offset = 25, Required = false)]
		public UnderlyingDeliveryStreamComponent? UnderlyingDeliveryStream {get; set;}
		
		[Component(Offset = 26, Required = false)]
		public UnderlyingDeliveryScheduleGrpComponent? UnderlyingDeliveryScheduleGrp {get; set;}
		
		[TagDetails(Tag = 40547, Type = TagType.String, Offset = 27, Required = false)]
		public string? UnderlyingStreamText {get; set;}
		
		[TagDetails(Tag = 40988, Type = TagType.Length, Offset = 28, Required = false, LinksToTag = 40989)]
		public int? EncodedUnderlyingStreamTextLen {get; set;}
		
		[TagDetails(Tag = 40989, Type = TagType.RawData, Offset = 29, Required = false, LinksToTag = 40988)]
		public byte[]? EncodedUnderlyingStreamText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamType is not null) writer.WriteWholeNumber(40541, UnderlyingStreamType.Value);
			if (UnderlyingStreamXID is not null) writer.WriteString(42016, UnderlyingStreamXID);
			if (UnderlyingStreamDesc is not null) writer.WriteString(40542, UnderlyingStreamDesc);
			if (UnderlyingStreamVersion is not null) writer.WriteString(43083, UnderlyingStreamVersion);
			if (UnderlyingStreamVersionEffectiveDate is not null) writer.WriteLocalDateOnly(43084, UnderlyingStreamVersionEffectiveDate.Value);
			if (UnderlyingStreamPaySide is not null) writer.WriteWholeNumber(40543, UnderlyingStreamPaySide.Value);
			if (UnderlyingStreamReceiveSide is not null) writer.WriteWholeNumber(40544, UnderlyingStreamReceiveSide.Value);
			if (UnderlyingStreamNotionalXIDRef is not null) writer.WriteString(42018, UnderlyingStreamNotionalXIDRef);
			if (UnderlyingStreamNotional is not null) writer.WriteNumber(40545, UnderlyingStreamNotional.Value);
			if (UnderlyingStreamCurrency is not null) writer.WriteString(40546, UnderlyingStreamCurrency);
			if (UnderlyingStreamNotionalDeterminationMethod is not null) writer.WriteString(43085, UnderlyingStreamNotionalDeterminationMethod);
			if (UnderlyingStreamNotionalAdjustments is not null) writer.WriteWholeNumber(43086, UnderlyingStreamNotionalAdjustments.Value);
			if (UnderlyingStreamNotionalFrequencyPeriod is not null) writer.WriteWholeNumber(42019, UnderlyingStreamNotionalFrequencyPeriod.Value);
			if (UnderlyingStreamNotionalFrequencyUnit is not null) writer.WriteString(42020, UnderlyingStreamNotionalFrequencyUnit);
			if (UnderlyingStreamNotionalCommodityFrequency is not null) writer.WriteWholeNumber(42021, UnderlyingStreamNotionalCommodityFrequency.Value);
			if (UnderlyingStreamNotionalUnitOfMeasure is not null) writer.WriteString(42022, UnderlyingStreamNotionalUnitOfMeasure);
			if (UnderlyingStreamTotalNotional is not null) writer.WriteNumber(42023, UnderlyingStreamTotalNotional.Value);
			if (UnderlyingStreamTotalNotionalUnitOfMeasure is not null) writer.WriteString(42024, UnderlyingStreamTotalNotionalUnitOfMeasure);
			if (UnderlyingStreamCommodity is not null) ((IFixEncoder)UnderlyingStreamCommodity).Encode(writer);
			if (UnderlyingStreamEffectiveDate is not null) ((IFixEncoder)UnderlyingStreamEffectiveDate).Encode(writer);
			if (UnderlyingStreamTerminationDate is not null) ((IFixEncoder)UnderlyingStreamTerminationDate).Encode(writer);
			if (UnderlyingStreamCalculationPeriodDates is not null) ((IFixEncoder)UnderlyingStreamCalculationPeriodDates).Encode(writer);
			if (UnderlyingPaymentStream is not null) ((IFixEncoder)UnderlyingPaymentStream).Encode(writer);
			if (UnderlyingPaymentScheduleGrp is not null) ((IFixEncoder)UnderlyingPaymentScheduleGrp).Encode(writer);
			if (UnderlyingPaymentStubGrp is not null) ((IFixEncoder)UnderlyingPaymentStubGrp).Encode(writer);
			if (UnderlyingDeliveryStream is not null) ((IFixEncoder)UnderlyingDeliveryStream).Encode(writer);
			if (UnderlyingDeliveryScheduleGrp is not null) ((IFixEncoder)UnderlyingDeliveryScheduleGrp).Encode(writer);
			if (UnderlyingStreamText is not null) writer.WriteString(40547, UnderlyingStreamText);
			if (EncodedUnderlyingStreamText is not null)
			{
				writer.WriteWholeNumber(40988, EncodedUnderlyingStreamText.Length);
				writer.WriteBuffer(40989, EncodedUnderlyingStreamText);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamType = view.GetInt32(40541);
			UnderlyingStreamXID = view.GetString(42016);
			UnderlyingStreamDesc = view.GetString(40542);
			UnderlyingStreamVersion = view.GetString(43083);
			UnderlyingStreamVersionEffectiveDate = view.GetDateOnly(43084);
			UnderlyingStreamPaySide = view.GetInt32(40543);
			UnderlyingStreamReceiveSide = view.GetInt32(40544);
			UnderlyingStreamNotionalXIDRef = view.GetString(42018);
			UnderlyingStreamNotional = view.GetDouble(40545);
			UnderlyingStreamCurrency = view.GetString(40546);
			UnderlyingStreamNotionalDeterminationMethod = view.GetString(43085);
			UnderlyingStreamNotionalAdjustments = view.GetInt32(43086);
			UnderlyingStreamNotionalFrequencyPeriod = view.GetInt32(42019);
			UnderlyingStreamNotionalFrequencyUnit = view.GetString(42020);
			UnderlyingStreamNotionalCommodityFrequency = view.GetInt32(42021);
			UnderlyingStreamNotionalUnitOfMeasure = view.GetString(42022);
			UnderlyingStreamTotalNotional = view.GetDouble(42023);
			UnderlyingStreamTotalNotionalUnitOfMeasure = view.GetString(42024);
			if (view.GetView("UnderlyingStreamCommodity") is IMessageView viewUnderlyingStreamCommodity)
			{
				UnderlyingStreamCommodity = new();
				((IFixParser)UnderlyingStreamCommodity).Parse(viewUnderlyingStreamCommodity);
			}
			if (view.GetView("UnderlyingStreamEffectiveDate") is IMessageView viewUnderlyingStreamEffectiveDate)
			{
				UnderlyingStreamEffectiveDate = new();
				((IFixParser)UnderlyingStreamEffectiveDate).Parse(viewUnderlyingStreamEffectiveDate);
			}
			if (view.GetView("UnderlyingStreamTerminationDate") is IMessageView viewUnderlyingStreamTerminationDate)
			{
				UnderlyingStreamTerminationDate = new();
				((IFixParser)UnderlyingStreamTerminationDate).Parse(viewUnderlyingStreamTerminationDate);
			}
			if (view.GetView("UnderlyingStreamCalculationPeriodDates") is IMessageView viewUnderlyingStreamCalculationPeriodDates)
			{
				UnderlyingStreamCalculationPeriodDates = new();
				((IFixParser)UnderlyingStreamCalculationPeriodDates).Parse(viewUnderlyingStreamCalculationPeriodDates);
			}
			if (view.GetView("UnderlyingPaymentStream") is IMessageView viewUnderlyingPaymentStream)
			{
				UnderlyingPaymentStream = new();
				((IFixParser)UnderlyingPaymentStream).Parse(viewUnderlyingPaymentStream);
			}
			if (view.GetView("UnderlyingPaymentScheduleGrp") is IMessageView viewUnderlyingPaymentScheduleGrp)
			{
				UnderlyingPaymentScheduleGrp = new();
				((IFixParser)UnderlyingPaymentScheduleGrp).Parse(viewUnderlyingPaymentScheduleGrp);
			}
			if (view.GetView("UnderlyingPaymentStubGrp") is IMessageView viewUnderlyingPaymentStubGrp)
			{
				UnderlyingPaymentStubGrp = new();
				((IFixParser)UnderlyingPaymentStubGrp).Parse(viewUnderlyingPaymentStubGrp);
			}
			if (view.GetView("UnderlyingDeliveryStream") is IMessageView viewUnderlyingDeliveryStream)
			{
				UnderlyingDeliveryStream = new();
				((IFixParser)UnderlyingDeliveryStream).Parse(viewUnderlyingDeliveryStream);
			}
			if (view.GetView("UnderlyingDeliveryScheduleGrp") is IMessageView viewUnderlyingDeliveryScheduleGrp)
			{
				UnderlyingDeliveryScheduleGrp = new();
				((IFixParser)UnderlyingDeliveryScheduleGrp).Parse(viewUnderlyingDeliveryScheduleGrp);
			}
			UnderlyingStreamText = view.GetString(40547);
			EncodedUnderlyingStreamTextLen = view.GetInt32(40988);
			EncodedUnderlyingStreamText = view.GetByteArray(40989);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamType":
					value = UnderlyingStreamType;
					break;
				case "UnderlyingStreamXID":
					value = UnderlyingStreamXID;
					break;
				case "UnderlyingStreamDesc":
					value = UnderlyingStreamDesc;
					break;
				case "UnderlyingStreamVersion":
					value = UnderlyingStreamVersion;
					break;
				case "UnderlyingStreamVersionEffectiveDate":
					value = UnderlyingStreamVersionEffectiveDate;
					break;
				case "UnderlyingStreamPaySide":
					value = UnderlyingStreamPaySide;
					break;
				case "UnderlyingStreamReceiveSide":
					value = UnderlyingStreamReceiveSide;
					break;
				case "UnderlyingStreamNotionalXIDRef":
					value = UnderlyingStreamNotionalXIDRef;
					break;
				case "UnderlyingStreamNotional":
					value = UnderlyingStreamNotional;
					break;
				case "UnderlyingStreamCurrency":
					value = UnderlyingStreamCurrency;
					break;
				case "UnderlyingStreamNotionalDeterminationMethod":
					value = UnderlyingStreamNotionalDeterminationMethod;
					break;
				case "UnderlyingStreamNotionalAdjustments":
					value = UnderlyingStreamNotionalAdjustments;
					break;
				case "UnderlyingStreamNotionalFrequencyPeriod":
					value = UnderlyingStreamNotionalFrequencyPeriod;
					break;
				case "UnderlyingStreamNotionalFrequencyUnit":
					value = UnderlyingStreamNotionalFrequencyUnit;
					break;
				case "UnderlyingStreamNotionalCommodityFrequency":
					value = UnderlyingStreamNotionalCommodityFrequency;
					break;
				case "UnderlyingStreamNotionalUnitOfMeasure":
					value = UnderlyingStreamNotionalUnitOfMeasure;
					break;
				case "UnderlyingStreamTotalNotional":
					value = UnderlyingStreamTotalNotional;
					break;
				case "UnderlyingStreamTotalNotionalUnitOfMeasure":
					value = UnderlyingStreamTotalNotionalUnitOfMeasure;
					break;
				case "UnderlyingStreamCommodity":
					value = UnderlyingStreamCommodity;
					break;
				case "UnderlyingStreamEffectiveDate":
					value = UnderlyingStreamEffectiveDate;
					break;
				case "UnderlyingStreamTerminationDate":
					value = UnderlyingStreamTerminationDate;
					break;
				case "UnderlyingStreamCalculationPeriodDates":
					value = UnderlyingStreamCalculationPeriodDates;
					break;
				case "UnderlyingPaymentStream":
					value = UnderlyingPaymentStream;
					break;
				case "UnderlyingPaymentScheduleGrp":
					value = UnderlyingPaymentScheduleGrp;
					break;
				case "UnderlyingPaymentStubGrp":
					value = UnderlyingPaymentStubGrp;
					break;
				case "UnderlyingDeliveryStream":
					value = UnderlyingDeliveryStream;
					break;
				case "UnderlyingDeliveryScheduleGrp":
					value = UnderlyingDeliveryScheduleGrp;
					break;
				case "UnderlyingStreamText":
					value = UnderlyingStreamText;
					break;
				case "EncodedUnderlyingStreamTextLen":
					value = EncodedUnderlyingStreamTextLen;
					break;
				case "EncodedUnderlyingStreamText":
					value = EncodedUnderlyingStreamText;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStreamType = null;
			UnderlyingStreamXID = null;
			UnderlyingStreamDesc = null;
			UnderlyingStreamVersion = null;
			UnderlyingStreamVersionEffectiveDate = null;
			UnderlyingStreamPaySide = null;
			UnderlyingStreamReceiveSide = null;
			UnderlyingStreamNotionalXIDRef = null;
			UnderlyingStreamNotional = null;
			UnderlyingStreamCurrency = null;
			UnderlyingStreamNotionalDeterminationMethod = null;
			UnderlyingStreamNotionalAdjustments = null;
			UnderlyingStreamNotionalFrequencyPeriod = null;
			UnderlyingStreamNotionalFrequencyUnit = null;
			UnderlyingStreamNotionalCommodityFrequency = null;
			UnderlyingStreamNotionalUnitOfMeasure = null;
			UnderlyingStreamTotalNotional = null;
			UnderlyingStreamTotalNotionalUnitOfMeasure = null;
			((IFixReset?)UnderlyingStreamCommodity)?.Reset();
			((IFixReset?)UnderlyingStreamEffectiveDate)?.Reset();
			((IFixReset?)UnderlyingStreamTerminationDate)?.Reset();
			((IFixReset?)UnderlyingStreamCalculationPeriodDates)?.Reset();
			((IFixReset?)UnderlyingPaymentStream)?.Reset();
			((IFixReset?)UnderlyingPaymentScheduleGrp)?.Reset();
			((IFixReset?)UnderlyingPaymentStubGrp)?.Reset();
			((IFixReset?)UnderlyingDeliveryStream)?.Reset();
			((IFixReset?)UnderlyingDeliveryScheduleGrp)?.Reset();
			UnderlyingStreamText = null;
			EncodedUnderlyingStreamTextLen = null;
			EncodedUnderlyingStreamText = null;
		}
	}
}
