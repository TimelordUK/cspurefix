using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamNonDeliverableFixingDatesBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40929, Offset = 0, Required = false)]
		public IOINoLegPaymentStreamNonDeliverableFixingDateBusinessCenters[]? NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters is not null && NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40929, NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters") is IMessageView viewNoLegPaymentStreamNonDeliverableFixingDateBusinessCenters)
			{
				var count = viewNoLegPaymentStreamNonDeliverableFixingDateBusinessCenters.GroupCount();
				NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters = new IOINoLegPaymentStreamNonDeliverableFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters[i]).Parse(viewNoLegPaymentStreamNonDeliverableFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters":
					value = NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamNonDeliverableFixingDateBusinessCenters = null;
		}
	}
}
