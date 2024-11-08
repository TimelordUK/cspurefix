using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40374, Offset = 0, Required = false)]
		public IOINoLegPaymentSchedules[]? NoLegPaymentSchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentSchedules is not null && NoLegPaymentSchedules.Length != 0)
			{
				writer.WriteWholeNumber(40374, NoLegPaymentSchedules.Length);
				for (int i = 0; i < NoLegPaymentSchedules.Length; i++)
				{
					((IFixEncoder)NoLegPaymentSchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentSchedules") is IMessageView viewNoLegPaymentSchedules)
			{
				var count = viewNoLegPaymentSchedules.GroupCount();
				NoLegPaymentSchedules = new IOINoLegPaymentSchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentSchedules[i] = new();
					((IFixParser)NoLegPaymentSchedules[i]).Parse(viewNoLegPaymentSchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentSchedules":
					value = NoLegPaymentSchedules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentSchedules = null;
		}
	}
}
