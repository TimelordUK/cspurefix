using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentScheduleRateSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40414, Offset = 0, Required = false)]
		public IOINoLegPaymentScheduleRateSources[]? NoLegPaymentScheduleRateSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentScheduleRateSources is not null && NoLegPaymentScheduleRateSources.Length != 0)
			{
				writer.WriteWholeNumber(40414, NoLegPaymentScheduleRateSources.Length);
				for (int i = 0; i < NoLegPaymentScheduleRateSources.Length; i++)
				{
					((IFixEncoder)NoLegPaymentScheduleRateSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentScheduleRateSources") is IMessageView viewNoLegPaymentScheduleRateSources)
			{
				var count = viewNoLegPaymentScheduleRateSources.GroupCount();
				NoLegPaymentScheduleRateSources = new IOINoLegPaymentScheduleRateSources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentScheduleRateSources[i] = new();
					((IFixParser)NoLegPaymentScheduleRateSources[i]).Parse(viewNoLegPaymentScheduleRateSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentScheduleRateSources":
					value = NoLegPaymentScheduleRateSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentScheduleRateSources = null;
		}
	}
}
