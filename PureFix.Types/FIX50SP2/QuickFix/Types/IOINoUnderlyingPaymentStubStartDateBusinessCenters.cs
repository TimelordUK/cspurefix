using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingPaymentStubStartDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 43001, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingPaymentStubStartDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStubStartDateBusinessCenter is not null) writer.WriteString(43001, UnderlyingPaymentStubStartDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStubStartDateBusinessCenter = view.GetString(43001);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStubStartDateBusinessCenter":
					value = UnderlyingPaymentStubStartDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingPaymentStubStartDateBusinessCenter = null;
		}
	}
}
