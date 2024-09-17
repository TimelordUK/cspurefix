using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentStubEndDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42496, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentStubEndDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStubEndDateBusinessCenter is not null) writer.WriteString(42496, LegPaymentStubEndDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStubEndDateBusinessCenter = view.GetString(42496);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStubEndDateBusinessCenter":
					value = LegPaymentStubEndDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
