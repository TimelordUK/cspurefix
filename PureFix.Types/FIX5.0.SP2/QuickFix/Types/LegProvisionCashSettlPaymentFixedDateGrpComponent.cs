using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionCashSettlPaymentFixedDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40473, Offset = 0, Required = false)]
		public NoLegProvisionCashSettlPaymentDates[]? NoLegProvisionCashSettlPaymentDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionCashSettlPaymentDates is not null && NoLegProvisionCashSettlPaymentDates.Length != 0)
			{
				writer.WriteWholeNumber(40473, NoLegProvisionCashSettlPaymentDates.Length);
				for (int i = 0; i < NoLegProvisionCashSettlPaymentDates.Length; i++)
				{
					((IFixEncoder)NoLegProvisionCashSettlPaymentDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionCashSettlPaymentDates") is IMessageView viewNoLegProvisionCashSettlPaymentDates)
			{
				var count = viewNoLegProvisionCashSettlPaymentDates.GroupCount();
				NoLegProvisionCashSettlPaymentDates = new NoLegProvisionCashSettlPaymentDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionCashSettlPaymentDates[i] = new();
					((IFixParser)NoLegProvisionCashSettlPaymentDates[i]).Parse(viewNoLegProvisionCashSettlPaymentDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionCashSettlPaymentDates":
					value = NoLegProvisionCashSettlPaymentDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
