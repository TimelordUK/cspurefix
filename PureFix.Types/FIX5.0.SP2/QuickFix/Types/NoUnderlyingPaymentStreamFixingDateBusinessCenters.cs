using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingPaymentStreamFixingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40607, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingPaymentStreamFixingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamFixingDateBusinessCenter is not null) writer.WriteString(40607, UnderlyingPaymentStreamFixingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamFixingDateBusinessCenter = view.GetString(40607);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamFixingDateBusinessCenter":
					value = UnderlyingPaymentStreamFixingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
