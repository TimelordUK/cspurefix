using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamResetDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40931, Offset = 0, Required = false)]
		public IOINoLegPaymentStreamResetDateBusinessCenters[]? NoLegPaymentStreamResetDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamResetDateBusinessCenters is not null && NoLegPaymentStreamResetDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40931, NoLegPaymentStreamResetDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStreamResetDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamResetDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamResetDateBusinessCenters") is IMessageView viewNoLegPaymentStreamResetDateBusinessCenters)
			{
				var count = viewNoLegPaymentStreamResetDateBusinessCenters.GroupCount();
				NoLegPaymentStreamResetDateBusinessCenters = new IOINoLegPaymentStreamResetDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamResetDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStreamResetDateBusinessCenters[i]).Parse(viewNoLegPaymentStreamResetDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamResetDateBusinessCenters":
					value = NoLegPaymentStreamResetDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamResetDateBusinessCenters = null;
		}
	}
}
