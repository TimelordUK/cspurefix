using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamInitialFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40932, Offset = 0, Required = false)]
		public IOINoLegPaymentStreamInitialFixingDateBusinessCenters[]? NoLegPaymentStreamInitialFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamInitialFixingDateBusinessCenters is not null && NoLegPaymentStreamInitialFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40932, NoLegPaymentStreamInitialFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStreamInitialFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamInitialFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamInitialFixingDateBusinessCenters") is IMessageView viewNoLegPaymentStreamInitialFixingDateBusinessCenters)
			{
				var count = viewNoLegPaymentStreamInitialFixingDateBusinessCenters.GroupCount();
				NoLegPaymentStreamInitialFixingDateBusinessCenters = new IOINoLegPaymentStreamInitialFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamInitialFixingDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStreamInitialFixingDateBusinessCenters[i]).Parse(viewNoLegPaymentStreamInitialFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamInitialFixingDateBusinessCenters":
					value = NoLegPaymentStreamInitialFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamInitialFixingDateBusinessCenters = null;
		}
	}
}
