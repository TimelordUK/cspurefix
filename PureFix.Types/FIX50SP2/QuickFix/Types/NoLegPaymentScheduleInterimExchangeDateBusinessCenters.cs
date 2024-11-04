using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentScheduleInterimExchangeDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40409, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentScheduleInterimExchangeDatesBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentScheduleInterimExchangeDatesBusinessCenter is not null) writer.WriteString(40409, LegPaymentScheduleInterimExchangeDatesBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentScheduleInterimExchangeDatesBusinessCenter = view.GetString(40409);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentScheduleInterimExchangeDatesBusinessCenter":
					value = LegPaymentScheduleInterimExchangeDatesBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentScheduleInterimExchangeDatesBusinessCenter = null;
		}
	}
}
