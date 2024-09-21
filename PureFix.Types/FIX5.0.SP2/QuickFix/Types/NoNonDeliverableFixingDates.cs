using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoNonDeliverableFixingDates : IFixGroup
	{
		[TagDetails(Tag = 40826, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? NonDeliverableFixingDate {get; set;}
		
		[TagDetails(Tag = 40827, Type = TagType.Int, Offset = 1, Required = false)]
		public int? NonDeliverableFixingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NonDeliverableFixingDate is not null) writer.WriteLocalDateOnly(40826, NonDeliverableFixingDate.Value);
			if (NonDeliverableFixingDateType is not null) writer.WriteWholeNumber(40827, NonDeliverableFixingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			NonDeliverableFixingDate = view.GetDateOnly(40826);
			NonDeliverableFixingDateType = view.GetInt32(40827);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NonDeliverableFixingDate":
					value = NonDeliverableFixingDate;
					break;
				case "NonDeliverableFixingDateType":
					value = NonDeliverableFixingDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NonDeliverableFixingDate = null;
			NonDeliverableFixingDateType = null;
		}
	}
}
