using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPositions : IFixGroup
	{
		[TagDetails(Tag = 703, Type = TagType.String, Offset = 0, Required = false)]
		public string? PosType {get; set;}
		
		[TagDetails(Tag = 704, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LongQty {get; set;}
		
		[TagDetails(Tag = 705, Type = TagType.Float, Offset = 2, Required = false)]
		public double? ShortQty {get; set;}
		
		[TagDetails(Tag = 1654, Type = TagType.Float, Offset = 3, Required = false)]
		public double? CoveredQty {get; set;}
		
		[TagDetails(Tag = 706, Type = TagType.Int, Offset = 4, Required = false)]
		public int? PosQtyStatus {get; set;}
		
		[TagDetails(Tag = 976, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? QuantityDate {get; set;}
		
		[TagDetails(Tag = 1836, Type = TagType.String, Offset = 6, Required = false)]
		public string? PosQtyUnitOfMeasure {get; set;}
		
		[TagDetails(Tag = 1835, Type = TagType.String, Offset = 7, Required = false)]
		public string? PosQtyUnitOfMeasureCurrency {get; set;}
		
		[TagDetails(Tag = 2936, Type = TagType.String, Offset = 8, Required = false)]
		public string? PosQtyUnitOfMeasureCurrencyCodeSource {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public NestedPartiesComponent? NestedParties {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PosType is not null) writer.WriteString(703, PosType);
			if (LongQty is not null) writer.WriteNumber(704, LongQty.Value);
			if (ShortQty is not null) writer.WriteNumber(705, ShortQty.Value);
			if (CoveredQty is not null) writer.WriteNumber(1654, CoveredQty.Value);
			if (PosQtyStatus is not null) writer.WriteWholeNumber(706, PosQtyStatus.Value);
			if (QuantityDate is not null) writer.WriteLocalDateOnly(976, QuantityDate.Value);
			if (PosQtyUnitOfMeasure is not null) writer.WriteString(1836, PosQtyUnitOfMeasure);
			if (PosQtyUnitOfMeasureCurrency is not null) writer.WriteString(1835, PosQtyUnitOfMeasureCurrency);
			if (PosQtyUnitOfMeasureCurrencyCodeSource is not null) writer.WriteString(2936, PosQtyUnitOfMeasureCurrencyCodeSource);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PosType = view.GetString(703);
			LongQty = view.GetDouble(704);
			ShortQty = view.GetDouble(705);
			CoveredQty = view.GetDouble(1654);
			PosQtyStatus = view.GetInt32(706);
			QuantityDate = view.GetDateOnly(976);
			PosQtyUnitOfMeasure = view.GetString(1836);
			PosQtyUnitOfMeasureCurrency = view.GetString(1835);
			PosQtyUnitOfMeasureCurrencyCodeSource = view.GetString(2936);
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				NestedParties = new();
				((IFixParser)NestedParties).Parse(viewNestedParties);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PosType":
					value = PosType;
					break;
				case "LongQty":
					value = LongQty;
					break;
				case "ShortQty":
					value = ShortQty;
					break;
				case "CoveredQty":
					value = CoveredQty;
					break;
				case "PosQtyStatus":
					value = PosQtyStatus;
					break;
				case "QuantityDate":
					value = QuantityDate;
					break;
				case "PosQtyUnitOfMeasure":
					value = PosQtyUnitOfMeasure;
					break;
				case "PosQtyUnitOfMeasureCurrency":
					value = PosQtyUnitOfMeasureCurrency;
					break;
				case "PosQtyUnitOfMeasureCurrencyCodeSource":
					value = PosQtyUnitOfMeasureCurrencyCodeSource;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				default: return false;
			}
			return true;
		}
	}
}
