using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionCashSettlValueDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40953, Offset = 0, Required = false)]
		public NoProvisionCashSettlValueDateBusinessCenters[]? NoProvisionCashSettlValueDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionCashSettlValueDateBusinessCenters is not null && NoProvisionCashSettlValueDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40953, NoProvisionCashSettlValueDateBusinessCenters.Length);
				for (int i = 0; i < NoProvisionCashSettlValueDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoProvisionCashSettlValueDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionCashSettlValueDateBusinessCenters") is IMessageView viewNoProvisionCashSettlValueDateBusinessCenters)
			{
				var count = viewNoProvisionCashSettlValueDateBusinessCenters.GroupCount();
				NoProvisionCashSettlValueDateBusinessCenters = new NoProvisionCashSettlValueDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionCashSettlValueDateBusinessCenters[i] = new();
					((IFixParser)NoProvisionCashSettlValueDateBusinessCenters[i]).Parse(viewNoProvisionCashSettlValueDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionCashSettlValueDateBusinessCenters":
					value = NoProvisionCashSettlValueDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
