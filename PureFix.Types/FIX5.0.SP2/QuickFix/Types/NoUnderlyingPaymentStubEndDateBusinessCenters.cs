using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingPaymentStubEndDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42992, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingPaymentStubEndDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStubEndDateBusinessCenter is not null) writer.WriteString(42992, UnderlyingPaymentStubEndDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStubEndDateBusinessCenter = view.GetString(42992);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStubEndDateBusinessCenter":
					value = UnderlyingPaymentStubEndDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
