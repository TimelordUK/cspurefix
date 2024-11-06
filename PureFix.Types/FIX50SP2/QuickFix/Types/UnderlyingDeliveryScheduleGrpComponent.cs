using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDeliveryScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41756, Offset = 0, Required = false)]
		public IOINoUnderlyingDeliverySchedules[]? NoUnderlyingDeliverySchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDeliverySchedules is not null && NoUnderlyingDeliverySchedules.Length != 0)
			{
				writer.WriteWholeNumber(41756, NoUnderlyingDeliverySchedules.Length);
				for (int i = 0; i < NoUnderlyingDeliverySchedules.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDeliverySchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDeliverySchedules") is IMessageView viewNoUnderlyingDeliverySchedules)
			{
				var count = viewNoUnderlyingDeliverySchedules.GroupCount();
				NoUnderlyingDeliverySchedules = new IOINoUnderlyingDeliverySchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDeliverySchedules[i] = new();
					((IFixParser)NoUnderlyingDeliverySchedules[i]).Parse(viewNoUnderlyingDeliverySchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDeliverySchedules":
					value = NoUnderlyingDeliverySchedules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingDeliverySchedules = null;
		}
	}
}
