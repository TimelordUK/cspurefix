using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoComplexEventAveragingObservations : IFixGroup
	{
		[TagDetails(Tag = 40995, Type = TagType.Int, Offset = 0, Required = false)]
		public int? ComplexEventAveragingObservationNumber {get; set;}
		
		[TagDetails(Tag = 40996, Type = TagType.Float, Offset = 1, Required = false)]
		public double? ComplexEventAveragingWeight {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ComplexEventAveragingObservationNumber is not null) writer.WriteWholeNumber(40995, ComplexEventAveragingObservationNumber.Value);
			if (ComplexEventAveragingWeight is not null) writer.WriteNumber(40996, ComplexEventAveragingWeight.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ComplexEventAveragingObservationNumber = view.GetInt32(40995);
			ComplexEventAveragingWeight = view.GetDouble(40996);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ComplexEventAveragingObservationNumber":
					value = ComplexEventAveragingObservationNumber;
					break;
				case "ComplexEventAveragingWeight":
					value = ComplexEventAveragingWeight;
					break;
				default: return false;
			}
			return true;
		}
	}
}
