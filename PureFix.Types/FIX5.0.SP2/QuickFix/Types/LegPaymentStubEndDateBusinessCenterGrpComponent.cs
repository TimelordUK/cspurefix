using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStubEndDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42495, Offset = 0, Required = false)]
		public NoLegPaymentStubEndDateBusinessCenters[]? NoLegPaymentStubEndDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStubEndDateBusinessCenters is not null && NoLegPaymentStubEndDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42495, NoLegPaymentStubEndDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStubEndDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStubEndDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStubEndDateBusinessCenters") is IMessageView viewNoLegPaymentStubEndDateBusinessCenters)
			{
				var count = viewNoLegPaymentStubEndDateBusinessCenters.GroupCount();
				NoLegPaymentStubEndDateBusinessCenters = new NoLegPaymentStubEndDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStubEndDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStubEndDateBusinessCenters[i]).Parse(viewNoLegPaymentStubEndDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStubEndDateBusinessCenters":
					value = NoLegPaymentStubEndDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
