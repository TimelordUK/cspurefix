using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegDividendAccrualPaymentDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42311, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegDividendAccrualPaymentDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegDividendAccrualPaymentDateBusinessCenter is not null) writer.WriteString(42311, LegDividendAccrualPaymentDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegDividendAccrualPaymentDateBusinessCenter = view.GetString(42311);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegDividendAccrualPaymentDateBusinessCenter":
					value = LegDividendAccrualPaymentDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegDividendAccrualPaymentDateBusinessCenter = null;
		}
	}
}
