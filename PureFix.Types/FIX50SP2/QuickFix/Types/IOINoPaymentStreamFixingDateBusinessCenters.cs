using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoPaymentStreamFixingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40776, Type = TagType.String, Offset = 0, Required = false)]
		public string? PaymentStreamFixingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentStreamFixingDateBusinessCenter is not null) writer.WriteString(40776, PaymentStreamFixingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentStreamFixingDateBusinessCenter = view.GetString(40776);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentStreamFixingDateBusinessCenter":
					value = PaymentStreamFixingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PaymentStreamFixingDateBusinessCenter = null;
		}
	}
}
