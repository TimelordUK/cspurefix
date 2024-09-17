using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionCashSettlPaymentFixedDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42099, Offset = 0, Required = false)]
		public NoUnderlyingProvisionCashSettlPaymentDates[]? NoUnderlyingProvisionCashSettlPaymentDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionCashSettlPaymentDates is not null && NoUnderlyingProvisionCashSettlPaymentDates.Length != 0)
			{
				writer.WriteWholeNumber(42099, NoUnderlyingProvisionCashSettlPaymentDates.Length);
				for (int i = 0; i < NoUnderlyingProvisionCashSettlPaymentDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionCashSettlPaymentDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionCashSettlPaymentDates") is IMessageView viewNoUnderlyingProvisionCashSettlPaymentDates)
			{
				var count = viewNoUnderlyingProvisionCashSettlPaymentDates.GroupCount();
				NoUnderlyingProvisionCashSettlPaymentDates = new NoUnderlyingProvisionCashSettlPaymentDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionCashSettlPaymentDates[i] = new();
					((IFixParser)NoUnderlyingProvisionCashSettlPaymentDates[i]).Parse(viewNoUnderlyingProvisionCashSettlPaymentDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionCashSettlPaymentDates":
					value = NoUnderlyingProvisionCashSettlPaymentDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
