using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamCompoundingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42405, Offset = 0, Required = false)]
		public IOINoLegPaymentStreamCompoundingDates[]? NoLegPaymentStreamCompoundingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamCompoundingDates is not null && NoLegPaymentStreamCompoundingDates.Length != 0)
			{
				writer.WriteWholeNumber(42405, NoLegPaymentStreamCompoundingDates.Length);
				for (int i = 0; i < NoLegPaymentStreamCompoundingDates.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamCompoundingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamCompoundingDates") is IMessageView viewNoLegPaymentStreamCompoundingDates)
			{
				var count = viewNoLegPaymentStreamCompoundingDates.GroupCount();
				NoLegPaymentStreamCompoundingDates = new IOINoLegPaymentStreamCompoundingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamCompoundingDates[i] = new();
					((IFixParser)NoLegPaymentStreamCompoundingDates[i]).Parse(viewNoLegPaymentStreamCompoundingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamCompoundingDates":
					value = NoLegPaymentStreamCompoundingDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamCompoundingDates = null;
		}
	}
}
