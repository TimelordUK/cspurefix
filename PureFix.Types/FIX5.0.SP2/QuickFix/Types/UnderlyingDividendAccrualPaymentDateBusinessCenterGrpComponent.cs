using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDividendAccrualPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42799, Offset = 0, Required = false)]
		public NoUnderlyingDividendAccrualPaymentDateBusinessCenters[]? NoUnderlyingDividendAccrualPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDividendAccrualPaymentDateBusinessCenters is not null && NoUnderlyingDividendAccrualPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42799, NoUnderlyingDividendAccrualPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingDividendAccrualPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDividendAccrualPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDividendAccrualPaymentDateBusinessCenters") is IMessageView viewNoUnderlyingDividendAccrualPaymentDateBusinessCenters)
			{
				var count = viewNoUnderlyingDividendAccrualPaymentDateBusinessCenters.GroupCount();
				NoUnderlyingDividendAccrualPaymentDateBusinessCenters = new NoUnderlyingDividendAccrualPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDividendAccrualPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingDividendAccrualPaymentDateBusinessCenters[i]).Parse(viewNoUnderlyingDividendAccrualPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDividendAccrualPaymentDateBusinessCenters":
					value = NoUnderlyingDividendAccrualPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
