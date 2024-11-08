using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentScheduleInterimExchangeDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40928, Offset = 0, Required = false)]
		public IOINoLegPaymentScheduleInterimExchangeDateBusinessCenters[]? NoLegPaymentScheduleInterimExchangeDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentScheduleInterimExchangeDateBusinessCenters is not null && NoLegPaymentScheduleInterimExchangeDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40928, NoLegPaymentScheduleInterimExchangeDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentScheduleInterimExchangeDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentScheduleInterimExchangeDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentScheduleInterimExchangeDateBusinessCenters") is IMessageView viewNoLegPaymentScheduleInterimExchangeDateBusinessCenters)
			{
				var count = viewNoLegPaymentScheduleInterimExchangeDateBusinessCenters.GroupCount();
				NoLegPaymentScheduleInterimExchangeDateBusinessCenters = new IOINoLegPaymentScheduleInterimExchangeDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentScheduleInterimExchangeDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentScheduleInterimExchangeDateBusinessCenters[i]).Parse(viewNoLegPaymentScheduleInterimExchangeDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentScheduleInterimExchangeDateBusinessCenters":
					value = NoLegPaymentScheduleInterimExchangeDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentScheduleInterimExchangeDateBusinessCenters = null;
		}
	}
}
