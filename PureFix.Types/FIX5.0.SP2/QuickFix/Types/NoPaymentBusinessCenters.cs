using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPaymentBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40221, Type = TagType.String, Offset = 0, Required = false)]
		public string? PaymentBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentBusinessCenter is not null) writer.WriteString(40221, PaymentBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentBusinessCenter = view.GetString(40221);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentBusinessCenter":
					value = PaymentBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
