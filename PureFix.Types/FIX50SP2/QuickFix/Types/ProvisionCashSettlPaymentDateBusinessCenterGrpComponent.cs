using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionCashSettlPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40952, Offset = 0, Required = false)]
		public IOINoProvisionCashSettlPaymentDateBusinessCenters[]? NoProvisionCashSettlPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionCashSettlPaymentDateBusinessCenters is not null && NoProvisionCashSettlPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40952, NoProvisionCashSettlPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoProvisionCashSettlPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoProvisionCashSettlPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionCashSettlPaymentDateBusinessCenters") is IMessageView viewNoProvisionCashSettlPaymentDateBusinessCenters)
			{
				var count = viewNoProvisionCashSettlPaymentDateBusinessCenters.GroupCount();
				NoProvisionCashSettlPaymentDateBusinessCenters = new IOINoProvisionCashSettlPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionCashSettlPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoProvisionCashSettlPaymentDateBusinessCenters[i]).Parse(viewNoProvisionCashSettlPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionCashSettlPaymentDateBusinessCenters":
					value = NoProvisionCashSettlPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProvisionCashSettlPaymentDateBusinessCenters = null;
		}
	}
}
