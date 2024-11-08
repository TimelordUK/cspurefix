using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegPaymentScheduleFixingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40400, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentScheduleFixingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentScheduleFixingDateBusinessCenter is not null) writer.WriteString(40400, LegPaymentScheduleFixingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentScheduleFixingDateBusinessCenter = view.GetString(40400);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentScheduleFixingDateBusinessCenter":
					value = LegPaymentScheduleFixingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentScheduleFixingDateBusinessCenter = null;
		}
	}
}
