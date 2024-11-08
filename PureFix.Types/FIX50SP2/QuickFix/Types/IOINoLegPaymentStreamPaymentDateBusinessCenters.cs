using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegPaymentStreamPaymentDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40293, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentStreamPaymentDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamPaymentDateBusinessCenter is not null) writer.WriteString(40293, LegPaymentStreamPaymentDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamPaymentDateBusinessCenter = view.GetString(40293);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamPaymentDateBusinessCenter":
					value = LegPaymentStreamPaymentDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentStreamPaymentDateBusinessCenter = null;
		}
	}
}
