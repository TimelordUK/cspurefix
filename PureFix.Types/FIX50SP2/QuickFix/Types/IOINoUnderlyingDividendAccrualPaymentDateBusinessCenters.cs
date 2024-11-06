using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingDividendAccrualPaymentDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42800, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingDividendAccrualPaymentDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDividendAccrualPaymentDateBusinessCenter is not null) writer.WriteString(42800, UnderlyingDividendAccrualPaymentDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDividendAccrualPaymentDateBusinessCenter = view.GetString(42800);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDividendAccrualPaymentDateBusinessCenter":
					value = UnderlyingDividendAccrualPaymentDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingDividendAccrualPaymentDateBusinessCenter = null;
		}
	}
}
