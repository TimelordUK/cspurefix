using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoProvisionCashSettlValueDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40117, Type = TagType.String, Offset = 0, Required = false)]
		public string? ProvisionCashSettlValueDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProvisionCashSettlValueDateBusinessCenter is not null) writer.WriteString(40117, ProvisionCashSettlValueDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProvisionCashSettlValueDateBusinessCenter = view.GetString(40117);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProvisionCashSettlValueDateBusinessCenter":
					value = ProvisionCashSettlValueDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
