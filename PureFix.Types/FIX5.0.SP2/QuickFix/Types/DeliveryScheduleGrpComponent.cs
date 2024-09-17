using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DeliveryScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41037, Offset = 0, Required = false)]
		public NoDeliverySchedules[]? NoDeliverySchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDeliverySchedules is not null && NoDeliverySchedules.Length != 0)
			{
				writer.WriteWholeNumber(41037, NoDeliverySchedules.Length);
				for (int i = 0; i < NoDeliverySchedules.Length; i++)
				{
					((IFixEncoder)NoDeliverySchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDeliverySchedules") is IMessageView viewNoDeliverySchedules)
			{
				var count = viewNoDeliverySchedules.GroupCount();
				NoDeliverySchedules = new NoDeliverySchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoDeliverySchedules[i] = new();
					((IFixParser)NoDeliverySchedules[i]).Parse(viewNoDeliverySchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDeliverySchedules":
					value = NoDeliverySchedules;
					break;
				default: return false;
			}
			return true;
		}
	}
}
