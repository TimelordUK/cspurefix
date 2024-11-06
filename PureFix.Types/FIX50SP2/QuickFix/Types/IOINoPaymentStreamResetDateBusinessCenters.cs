using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoPaymentStreamResetDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40763, Type = TagType.String, Offset = 0, Required = false)]
		public string? PaymentStreamResetDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentStreamResetDateBusinessCenter is not null) writer.WriteString(40763, PaymentStreamResetDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentStreamResetDateBusinessCenter = view.GetString(40763);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentStreamResetDateBusinessCenter":
					value = PaymentStreamResetDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PaymentStreamResetDateBusinessCenter = null;
		}
	}
}
