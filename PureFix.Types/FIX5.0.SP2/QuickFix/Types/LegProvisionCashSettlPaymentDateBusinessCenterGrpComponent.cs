using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionCashSettlPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40934, Offset = 0, Required = false)]
		public NoLegProvisionCashSettlPaymentDateBusinessCenters[]? NoLegProvisionCashSettlPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionCashSettlPaymentDateBusinessCenters is not null && NoLegProvisionCashSettlPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40934, NoLegProvisionCashSettlPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoLegProvisionCashSettlPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegProvisionCashSettlPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionCashSettlPaymentDateBusinessCenters") is IMessageView viewNoLegProvisionCashSettlPaymentDateBusinessCenters)
			{
				var count = viewNoLegProvisionCashSettlPaymentDateBusinessCenters.GroupCount();
				NoLegProvisionCashSettlPaymentDateBusinessCenters = new NoLegProvisionCashSettlPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionCashSettlPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoLegProvisionCashSettlPaymentDateBusinessCenters[i]).Parse(viewNoLegProvisionCashSettlPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionCashSettlPaymentDateBusinessCenters":
					value = NoLegProvisionCashSettlPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
