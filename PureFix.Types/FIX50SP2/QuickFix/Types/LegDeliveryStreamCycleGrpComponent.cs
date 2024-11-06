using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDeliveryStreamCycleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41456, Offset = 0, Required = false)]
		public IOINoLegDeliveryStreamCycles[]? NoLegDeliveryStreamCycles {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDeliveryStreamCycles is not null && NoLegDeliveryStreamCycles.Length != 0)
			{
				writer.WriteWholeNumber(41456, NoLegDeliveryStreamCycles.Length);
				for (int i = 0; i < NoLegDeliveryStreamCycles.Length; i++)
				{
					((IFixEncoder)NoLegDeliveryStreamCycles[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDeliveryStreamCycles") is IMessageView viewNoLegDeliveryStreamCycles)
			{
				var count = viewNoLegDeliveryStreamCycles.GroupCount();
				NoLegDeliveryStreamCycles = new IOINoLegDeliveryStreamCycles[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDeliveryStreamCycles[i] = new();
					((IFixParser)NoLegDeliveryStreamCycles[i]).Parse(viewNoLegDeliveryStreamCycles.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDeliveryStreamCycles":
					value = NoLegDeliveryStreamCycles;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDeliveryStreamCycles = null;
		}
	}
}
