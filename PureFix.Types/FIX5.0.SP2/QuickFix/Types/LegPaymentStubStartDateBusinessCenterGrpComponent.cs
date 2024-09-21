using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStubStartDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42504, Offset = 0, Required = false)]
		public NoLegPaymentStubStartDateBusinessCenters[]? NoLegPaymentStubStartDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStubStartDateBusinessCenters is not null && NoLegPaymentStubStartDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42504, NoLegPaymentStubStartDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentStubStartDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStubStartDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStubStartDateBusinessCenters") is IMessageView viewNoLegPaymentStubStartDateBusinessCenters)
			{
				var count = viewNoLegPaymentStubStartDateBusinessCenters.GroupCount();
				NoLegPaymentStubStartDateBusinessCenters = new NoLegPaymentStubStartDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStubStartDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentStubStartDateBusinessCenters[i]).Parse(viewNoLegPaymentStubStartDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStubStartDateBusinessCenters":
					value = NoLegPaymentStubStartDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStubStartDateBusinessCenters = null;
		}
	}
}
