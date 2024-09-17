using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40933, Offset = 0, Required = false)]
		public NoLegPaymentStreamFixingDateBusinessCenters[]? NoLegPaymentStreamFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamFixingDateBusinessCenters is not null && NoLegPaymentStreamFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40933, NoLegPaymentStreamFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStreamFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamFixingDateBusinessCenters") is IMessageView viewNoLegPaymentStreamFixingDateBusinessCenters)
			{
				var count = viewNoLegPaymentStreamFixingDateBusinessCenters.GroupCount();
				NoLegPaymentStreamFixingDateBusinessCenters = new NoLegPaymentStreamFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamFixingDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStreamFixingDateBusinessCenters[i]).Parse(viewNoLegPaymentStreamFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamFixingDateBusinessCenters":
					value = NoLegPaymentStreamFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
