using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DividendAccrualPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42236, Offset = 0, Required = false)]
		public IOINoDividendAccrualPaymentDateBusinessCenters[]? NoDividendAccrualPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDividendAccrualPaymentDateBusinessCenters is not null && NoDividendAccrualPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42236, NoDividendAccrualPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoDividendAccrualPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoDividendAccrualPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDividendAccrualPaymentDateBusinessCenters") is IMessageView viewNoDividendAccrualPaymentDateBusinessCenters)
			{
				var count = viewNoDividendAccrualPaymentDateBusinessCenters.GroupCount();
				NoDividendAccrualPaymentDateBusinessCenters = new IOINoDividendAccrualPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoDividendAccrualPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoDividendAccrualPaymentDateBusinessCenters[i]).Parse(viewNoDividendAccrualPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDividendAccrualPaymentDateBusinessCenters":
					value = NoDividendAccrualPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDividendAccrualPaymentDateBusinessCenters = null;
		}
	}
}
