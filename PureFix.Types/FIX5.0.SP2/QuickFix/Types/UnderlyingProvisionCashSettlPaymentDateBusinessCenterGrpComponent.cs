using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionCashSettlPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42180, Offset = 0, Required = false)]
		public NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters[]? NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters is not null && NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42180, NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters") is IMessageView viewNoUnderlyingProvisionCashSettlPaymentDateBusinessCenters)
			{
				var count = viewNoUnderlyingProvisionCashSettlPaymentDateBusinessCenters.GroupCount();
				NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters = new NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters[i]).Parse(viewNoUnderlyingProvisionCashSettlPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters":
					value = NoUnderlyingProvisionCashSettlPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
