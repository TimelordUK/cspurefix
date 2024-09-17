using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPaymentStubEndDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42697, Type = TagType.String, Offset = 0, Required = false)]
		public string? PaymentStubEndDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentStubEndDateBusinessCenter is not null) writer.WriteString(42697, PaymentStubEndDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentStubEndDateBusinessCenter = view.GetString(42697);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentStubEndDateBusinessCenter":
					value = PaymentStubEndDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
