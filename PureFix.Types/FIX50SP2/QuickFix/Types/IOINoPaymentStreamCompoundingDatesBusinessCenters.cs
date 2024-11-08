using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoPaymentStreamCompoundingDatesBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42621, Type = TagType.String, Offset = 0, Required = false)]
		public string? PaymentStreamCompoundingDatesBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentStreamCompoundingDatesBusinessCenter is not null) writer.WriteString(42621, PaymentStreamCompoundingDatesBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentStreamCompoundingDatesBusinessCenter = view.GetString(42621);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentStreamCompoundingDatesBusinessCenter":
					value = PaymentStreamCompoundingDatesBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PaymentStreamCompoundingDatesBusinessCenter = null;
		}
	}
}
