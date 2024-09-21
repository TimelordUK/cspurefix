using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionCashSettlValueDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42182, Offset = 0, Required = false)]
		public NoUnderlyingProvisionCashSettlValueDateBusinessCenters[]? NoUnderlyingProvisionCashSettlValueDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionCashSettlValueDateBusinessCenters is not null && NoUnderlyingProvisionCashSettlValueDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42182, NoUnderlyingProvisionCashSettlValueDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingProvisionCashSettlValueDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionCashSettlValueDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionCashSettlValueDateBusinessCenters") is IMessageView viewNoUnderlyingProvisionCashSettlValueDateBusinessCenters)
			{
				var count = viewNoUnderlyingProvisionCashSettlValueDateBusinessCenters.GroupCount();
				NoUnderlyingProvisionCashSettlValueDateBusinessCenters = new NoUnderlyingProvisionCashSettlValueDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionCashSettlValueDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingProvisionCashSettlValueDateBusinessCenters[i]).Parse(viewNoUnderlyingProvisionCashSettlValueDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionCashSettlValueDateBusinessCenters":
					value = NoUnderlyingProvisionCashSettlValueDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProvisionCashSettlValueDateBusinessCenters = null;
		}
	}
}
