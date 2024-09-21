using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentStreamInitialFixingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40311, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPaymentStreamInitialFixingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamInitialFixingDateBusinessCenter is not null) writer.WriteString(40311, LegPaymentStreamInitialFixingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamInitialFixingDateBusinessCenter = view.GetString(40311);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamInitialFixingDateBusinessCenter":
					value = LegPaymentStreamInitialFixingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentStreamInitialFixingDateBusinessCenter = null;
		}
	}
}
