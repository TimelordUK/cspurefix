using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class AdjustedPositionReportPositionQty : IFixGroup
	{
		[TagDetails(Tag = 703, Type = TagType.String, Offset = 0, Required = false)]
		public string? PosType {get; set;}
		
		[TagDetails(Tag = 704, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LongQty {get; set;}
		
		[TagDetails(Tag = 705, Type = TagType.Float, Offset = 2, Required = false)]
		public double? ShortQty {get; set;}
		
		[TagDetails(Tag = 706, Type = TagType.Int, Offset = 3, Required = false)]
		public int? PosQtyStatus {get; set;}
		
		[TagDetails(Tag = 976, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? QuantityDate {get; set;}
		
		[Group(NoOfTag = 1008, Offset = 5, Required = false)]
		public AdjustedPositionReportNestedParties[]? NestedParties {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PosType is not null) writer.WriteString(703, PosType);
			if (LongQty is not null) writer.WriteNumber(704, LongQty.Value);
			if (ShortQty is not null) writer.WriteNumber(705, ShortQty.Value);
			if (PosQtyStatus is not null) writer.WriteWholeNumber(706, PosQtyStatus.Value);
			if (QuantityDate is not null) writer.WriteLocalDateOnly(976, QuantityDate.Value);
			if (NestedParties is not null && NestedParties.Length != 0)
			{
				writer.WriteWholeNumber(1008, NestedParties.Length);
				for (int i = 0; i < NestedParties.Length; i++)
				{
					((IFixEncoder)NestedParties[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PosType = view.GetString(703);
			LongQty = view.GetDouble(704);
			ShortQty = view.GetDouble(705);
			PosQtyStatus = view.GetInt32(706);
			QuantityDate = view.GetDateOnly(976);
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				var count = viewNestedParties.GroupCount();
				NestedParties = new AdjustedPositionReportNestedParties[count];
				for (int i = 0; i < count; i++)
				{
					NestedParties[i] = new();
					((IFixParser)NestedParties[i]).Parse(viewNestedParties.GetGroupInstance(i));
				}
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
				case "PosQtyStatus":
					value = PosQtyStatus;
					break;
				case "QuantityDate":
					value = QuantityDate;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PosType = null;
			LongQty = null;
			ShortQty = null;
			PosQtyStatus = null;
			QuantityDate = null;
			NestedParties = null;
		}
	}
}
