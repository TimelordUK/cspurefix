using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionCashSettlValueDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40935, Offset = 0, Required = false)]
		public NoLegProvisionCashSettlValueDateBusinessCenters[]? NoLegProvisionCashSettlValueDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionCashSettlValueDateBusinessCenters is not null && NoLegProvisionCashSettlValueDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40935, NoLegProvisionCashSettlValueDateBusinessCenters.Length);
				for (int i = 0; i < NoLegProvisionCashSettlValueDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegProvisionCashSettlValueDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionCashSettlValueDateBusinessCenters") is IMessageView viewNoLegProvisionCashSettlValueDateBusinessCenters)
			{
				var count = viewNoLegProvisionCashSettlValueDateBusinessCenters.GroupCount();
				NoLegProvisionCashSettlValueDateBusinessCenters = new NoLegProvisionCashSettlValueDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionCashSettlValueDateBusinessCenters[i] = new();
					((IFixParser)NoLegProvisionCashSettlValueDateBusinessCenters[i]).Parse(viewNoLegProvisionCashSettlValueDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionCashSettlValueDateBusinessCenters":
					value = NoLegProvisionCashSettlValueDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
