using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentStreamResetDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40305, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentStreamResetDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamResetDateBusinessCenter is not null) writer.WriteString(40305, LegPaymentStreamResetDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamResetDateBusinessCenter = view.GetString(40305);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamResetDateBusinessCenter":
					value = LegPaymentStreamResetDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
