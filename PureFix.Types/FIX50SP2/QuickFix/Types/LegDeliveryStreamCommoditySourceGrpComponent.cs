using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDeliveryStreamCommoditySourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41460, Offset = 0, Required = false)]
		public NoLegDeliveryStreamCommoditySources[]? NoLegDeliveryStreamCommoditySources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDeliveryStreamCommoditySources is not null && NoLegDeliveryStreamCommoditySources.Length != 0)
			{
				writer.WriteWholeNumber(41460, NoLegDeliveryStreamCommoditySources.Length);
				for (int i = 0; i < NoLegDeliveryStreamCommoditySources.Length; i++)
				{
					((IFixEncoder)NoLegDeliveryStreamCommoditySources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDeliveryStreamCommoditySources") is IMessageView viewNoLegDeliveryStreamCommoditySources)
			{
				var count = viewNoLegDeliveryStreamCommoditySources.GroupCount();
				NoLegDeliveryStreamCommoditySources = new NoLegDeliveryStreamCommoditySources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDeliveryStreamCommoditySources[i] = new();
					((IFixParser)NoLegDeliveryStreamCommoditySources[i]).Parse(viewNoLegDeliveryStreamCommoditySources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDeliveryStreamCommoditySources":
					value = NoLegDeliveryStreamCommoditySources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDeliveryStreamCommoditySources = null;
		}
	}
}
