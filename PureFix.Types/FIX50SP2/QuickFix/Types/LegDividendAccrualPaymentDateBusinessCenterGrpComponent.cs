using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDividendAccrualPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42310, Offset = 0, Required = false)]
		public NoLegDividendAccrualPaymentDateBusinessCenters[]? NoLegDividendAccrualPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDividendAccrualPaymentDateBusinessCenters is not null && NoLegDividendAccrualPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42310, NoLegDividendAccrualPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoLegDividendAccrualPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegDividendAccrualPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDividendAccrualPaymentDateBusinessCenters") is IMessageView viewNoLegDividendAccrualPaymentDateBusinessCenters)
			{
				var count = viewNoLegDividendAccrualPaymentDateBusinessCenters.GroupCount();
				NoLegDividendAccrualPaymentDateBusinessCenters = new NoLegDividendAccrualPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDividendAccrualPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoLegDividendAccrualPaymentDateBusinessCenters[i]).Parse(viewNoLegDividendAccrualPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDividendAccrualPaymentDateBusinessCenters":
					value = NoLegDividendAccrualPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDividendAccrualPaymentDateBusinessCenters = null;
		}
	}
}
