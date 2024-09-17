using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProvisionCashSettlValueDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42183, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProvisionCashSettlValueDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionCashSettlValueDateBusinessCenter is not null) writer.WriteString(42183, UnderlyingProvisionCashSettlValueDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionCashSettlValueDateBusinessCenter = view.GetString(42183);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionCashSettlValueDateBusinessCenter":
					value = UnderlyingProvisionCashSettlValueDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
