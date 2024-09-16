using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingRateSpreadSteps : IFixGroup
	{
		[TagDetails(Tag = 43006, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingRateSpreadStepDate {get; set;}
		
		[TagDetails(Tag = 43007, Type = TagType.Float, Offset = 1, Required = false)]
		public double? UnderlyingRateSpreadStepValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingRateSpreadStepDate is not null) writer.WriteLocalDateOnly(43006, UnderlyingRateSpreadStepDate.Value);
			if (UnderlyingRateSpreadStepValue is not null) writer.WriteNumber(43007, UnderlyingRateSpreadStepValue.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingRateSpreadStepDate = view.GetDateOnly(43006);
			UnderlyingRateSpreadStepValue = view.GetDouble(43007);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingRateSpreadStepDate":
					value = UnderlyingRateSpreadStepDate;
					break;
				case "UnderlyingRateSpreadStepValue":
					value = UnderlyingRateSpreadStepValue;
					break;
				default: return false;
			}
			return true;
		}
	}
}
