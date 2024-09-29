using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoProvisionCashSettlPaymentDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40164, Type = TagType.String, Offset = 0, Required = false)]
		public string? ProvisionCashSettlPaymentDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProvisionCashSettlPaymentDateBusinessCenter is not null) writer.WriteString(40164, ProvisionCashSettlPaymentDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProvisionCashSettlPaymentDateBusinessCenter = view.GetString(40164);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProvisionCashSettlPaymentDateBusinessCenter":
					value = ProvisionCashSettlPaymentDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ProvisionCashSettlPaymentDateBusinessCenter = null;
		}
	}
}
