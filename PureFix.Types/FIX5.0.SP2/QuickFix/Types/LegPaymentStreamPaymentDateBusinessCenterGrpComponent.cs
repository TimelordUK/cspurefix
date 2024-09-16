using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40930, Offset = 0, Required = false)]
		public NoLegPaymentStreamPaymentDateBusinessCenters[]? NoLegPaymentStreamPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamPaymentDateBusinessCenters is not null && NoLegPaymentStreamPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40930, NoLegPaymentStreamPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStreamPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamPaymentDateBusinessCenters") is IMessageView viewNoLegPaymentStreamPaymentDateBusinessCenters)
			{
				var count = viewNoLegPaymentStreamPaymentDateBusinessCenters.GroupCount();
				NoLegPaymentStreamPaymentDateBusinessCenters = new NoLegPaymentStreamPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStreamPaymentDateBusinessCenters[i]).Parse(viewNoLegPaymentStreamPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamPaymentDateBusinessCenters":
					value = NoLegPaymentStreamPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
