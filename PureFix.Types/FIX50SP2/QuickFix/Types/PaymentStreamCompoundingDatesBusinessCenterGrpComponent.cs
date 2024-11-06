using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamCompoundingDatesBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42620, Offset = 0, Required = false)]
		public IOINoPaymentStreamCompoundingDatesBusinessCenters[]? NoPaymentStreamCompoundingDatesBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamCompoundingDatesBusinessCenters is not null && NoPaymentStreamCompoundingDatesBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42620, NoPaymentStreamCompoundingDatesBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamCompoundingDatesBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamCompoundingDatesBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamCompoundingDatesBusinessCenters") is IMessageView viewNoPaymentStreamCompoundingDatesBusinessCenters)
			{
				var count = viewNoPaymentStreamCompoundingDatesBusinessCenters.GroupCount();
				NoPaymentStreamCompoundingDatesBusinessCenters = new IOINoPaymentStreamCompoundingDatesBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamCompoundingDatesBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamCompoundingDatesBusinessCenters[i]).Parse(viewNoPaymentStreamCompoundingDatesBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamCompoundingDatesBusinessCenters":
					value = NoPaymentStreamCompoundingDatesBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamCompoundingDatesBusinessCenters = null;
		}
	}
}
