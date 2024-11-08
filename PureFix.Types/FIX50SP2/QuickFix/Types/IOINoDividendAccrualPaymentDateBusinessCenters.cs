using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoDividendAccrualPaymentDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42237, Type = TagType.String, Offset = 0, Required = false)]
		public string? DividendAccrualPaymentDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DividendAccrualPaymentDateBusinessCenter is not null) writer.WriteString(42237, DividendAccrualPaymentDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DividendAccrualPaymentDateBusinessCenter = view.GetString(42237);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DividendAccrualPaymentDateBusinessCenter":
					value = DividendAccrualPaymentDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DividendAccrualPaymentDateBusinessCenter = null;
		}
	}
}
