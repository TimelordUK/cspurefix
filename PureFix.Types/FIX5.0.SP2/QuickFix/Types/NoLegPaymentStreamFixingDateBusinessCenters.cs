using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentStreamFixingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40318, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentStreamFixingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamFixingDateBusinessCenter is not null) writer.WriteString(40318, LegPaymentStreamFixingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamFixingDateBusinessCenter = view.GetString(40318);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamFixingDateBusinessCenter":
					value = LegPaymentStreamFixingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
