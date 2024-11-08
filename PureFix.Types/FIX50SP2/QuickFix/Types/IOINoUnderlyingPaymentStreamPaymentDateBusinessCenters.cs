using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingPaymentStreamPaymentDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40582, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingPaymentStreamPaymentDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamPaymentDateBusinessCenter is not null) writer.WriteString(40582, UnderlyingPaymentStreamPaymentDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamPaymentDateBusinessCenter = view.GetString(40582);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamPaymentDateBusinessCenter":
					value = UnderlyingPaymentStreamPaymentDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingPaymentStreamPaymentDateBusinessCenter = null;
		}
	}
}
