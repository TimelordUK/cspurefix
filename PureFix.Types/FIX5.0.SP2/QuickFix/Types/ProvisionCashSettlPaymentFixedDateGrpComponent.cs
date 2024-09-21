using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionCashSettlPaymentFixedDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40171, Offset = 0, Required = false)]
		public NoProvisionCashSettlPaymentDates[]? NoProvisionCashSettlPaymentDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionCashSettlPaymentDates is not null && NoProvisionCashSettlPaymentDates.Length != 0)
			{
				writer.WriteWholeNumber(40171, NoProvisionCashSettlPaymentDates.Length);
				for (int i = 0; i < NoProvisionCashSettlPaymentDates.Length; i++)
				{
					((IFixEncoder)NoProvisionCashSettlPaymentDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionCashSettlPaymentDates") is IMessageView viewNoProvisionCashSettlPaymentDates)
			{
				var count = viewNoProvisionCashSettlPaymentDates.GroupCount();
				NoProvisionCashSettlPaymentDates = new NoProvisionCashSettlPaymentDates[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionCashSettlPaymentDates[i] = new();
					((IFixParser)NoProvisionCashSettlPaymentDates[i]).Parse(viewNoProvisionCashSettlPaymentDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionCashSettlPaymentDates":
					value = NoProvisionCashSettlPaymentDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProvisionCashSettlPaymentDates = null;
		}
	}
}
