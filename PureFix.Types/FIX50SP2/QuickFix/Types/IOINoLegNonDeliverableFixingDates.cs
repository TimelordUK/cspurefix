using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegNonDeliverableFixingDates : IFixGroup
	{
		[TagDetails(Tag = 40368, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegNonDeliverableFixingDate {get; set;}
		
		[TagDetails(Tag = 40369, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegNonDeliverableFixingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegNonDeliverableFixingDate is not null) writer.WriteLocalDateOnly(40368, LegNonDeliverableFixingDate.Value);
			if (LegNonDeliverableFixingDateType is not null) writer.WriteWholeNumber(40369, LegNonDeliverableFixingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegNonDeliverableFixingDate = view.GetDateOnly(40368);
			LegNonDeliverableFixingDateType = view.GetInt32(40369);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegNonDeliverableFixingDate":
					value = LegNonDeliverableFixingDate;
					break;
				case "LegNonDeliverableFixingDateType":
					value = LegNonDeliverableFixingDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegNonDeliverableFixingDate = null;
			LegNonDeliverableFixingDateType = null;
		}
	}
}
