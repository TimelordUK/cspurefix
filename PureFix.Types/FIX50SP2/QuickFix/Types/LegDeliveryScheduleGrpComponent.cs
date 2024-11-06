using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDeliveryScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41408, Offset = 0, Required = false)]
		public IOINoLegDeliverySchedules[]? NoLegDeliverySchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDeliverySchedules is not null && NoLegDeliverySchedules.Length != 0)
			{
				writer.WriteWholeNumber(41408, NoLegDeliverySchedules.Length);
				for (int i = 0; i < NoLegDeliverySchedules.Length; i++)
				{
					((IFixEncoder)NoLegDeliverySchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDeliverySchedules") is IMessageView viewNoLegDeliverySchedules)
			{
				var count = viewNoLegDeliverySchedules.GroupCount();
				NoLegDeliverySchedules = new IOINoLegDeliverySchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDeliverySchedules[i] = new();
					((IFixParser)NoLegDeliverySchedules[i]).Parse(viewNoLegDeliverySchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDeliverySchedules":
					value = NoLegDeliverySchedules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDeliverySchedules = null;
		}
	}
}
