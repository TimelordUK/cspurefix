using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDeliveryStreamCycleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41804, Offset = 0, Required = false)]
		public NoUnderlyingDeliveryStreamCycles[]? NoUnderlyingDeliveryStreamCycles {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDeliveryStreamCycles is not null && NoUnderlyingDeliveryStreamCycles.Length != 0)
			{
				writer.WriteWholeNumber(41804, NoUnderlyingDeliveryStreamCycles.Length);
				for (int i = 0; i < NoUnderlyingDeliveryStreamCycles.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDeliveryStreamCycles[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDeliveryStreamCycles") is IMessageView viewNoUnderlyingDeliveryStreamCycles)
			{
				var count = viewNoUnderlyingDeliveryStreamCycles.GroupCount();
				NoUnderlyingDeliveryStreamCycles = new NoUnderlyingDeliveryStreamCycles[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDeliveryStreamCycles[i] = new();
					((IFixParser)NoUnderlyingDeliveryStreamCycles[i]).Parse(viewNoUnderlyingDeliveryStreamCycles.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDeliveryStreamCycles":
					value = NoUnderlyingDeliveryStreamCycles;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingDeliveryStreamCycles = null;
		}
	}
}
