using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamNonDeliverableFixingDatesBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40946, Offset = 0, Required = false)]
		public NoPaymentStreamNonDeliverableFixingDatesBusinessCenters[]? NoPaymentStreamNonDeliverableFixingDatesBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamNonDeliverableFixingDatesBusinessCenters is not null && NoPaymentStreamNonDeliverableFixingDatesBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40946, NoPaymentStreamNonDeliverableFixingDatesBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamNonDeliverableFixingDatesBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamNonDeliverableFixingDatesBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamNonDeliverableFixingDatesBusinessCenters") is IMessageView viewNoPaymentStreamNonDeliverableFixingDatesBusinessCenters)
			{
				var count = viewNoPaymentStreamNonDeliverableFixingDatesBusinessCenters.GroupCount();
				NoPaymentStreamNonDeliverableFixingDatesBusinessCenters = new NoPaymentStreamNonDeliverableFixingDatesBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamNonDeliverableFixingDatesBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamNonDeliverableFixingDatesBusinessCenters[i]).Parse(viewNoPaymentStreamNonDeliverableFixingDatesBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamNonDeliverableFixingDatesBusinessCenters":
					value = NoPaymentStreamNonDeliverableFixingDatesBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
