using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegPaymentScheduleRateSources : IFixGroup
	{
		[TagDetails(Tag = 40415, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegPaymentScheduleRateSource {get; set;}
		
		[TagDetails(Tag = 40416, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegPaymentScheduleRateSourceType {get; set;}
		
		[TagDetails(Tag = 40417, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegPaymentScheduleReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentScheduleRateSource is not null) writer.WriteWholeNumber(40415, LegPaymentScheduleRateSource.Value);
			if (LegPaymentScheduleRateSourceType is not null) writer.WriteWholeNumber(40416, LegPaymentScheduleRateSourceType.Value);
			if (LegPaymentScheduleReferencePage is not null) writer.WriteString(40417, LegPaymentScheduleReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentScheduleRateSource = view.GetInt32(40415);
			LegPaymentScheduleRateSourceType = view.GetInt32(40416);
			LegPaymentScheduleReferencePage = view.GetString(40417);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentScheduleRateSource":
					value = LegPaymentScheduleRateSource;
					break;
				case "LegPaymentScheduleRateSourceType":
					value = LegPaymentScheduleRateSourceType;
					break;
				case "LegPaymentScheduleReferencePage":
					value = LegPaymentScheduleReferencePage;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentScheduleRateSource = null;
			LegPaymentScheduleRateSourceType = null;
			LegPaymentScheduleReferencePage = null;
		}
	}
}
