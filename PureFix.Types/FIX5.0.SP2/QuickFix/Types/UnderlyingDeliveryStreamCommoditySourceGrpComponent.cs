using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDeliveryStreamCommoditySourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41808, Offset = 0, Required = false)]
		public NoUnderlyingDeliveryStreamCommoditySources[]? NoUnderlyingDeliveryStreamCommoditySources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDeliveryStreamCommoditySources is not null && NoUnderlyingDeliveryStreamCommoditySources.Length != 0)
			{
				writer.WriteWholeNumber(41808, NoUnderlyingDeliveryStreamCommoditySources.Length);
				for (int i = 0; i < NoUnderlyingDeliveryStreamCommoditySources.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDeliveryStreamCommoditySources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDeliveryStreamCommoditySources") is IMessageView viewNoUnderlyingDeliveryStreamCommoditySources)
			{
				var count = viewNoUnderlyingDeliveryStreamCommoditySources.GroupCount();
				NoUnderlyingDeliveryStreamCommoditySources = new NoUnderlyingDeliveryStreamCommoditySources[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDeliveryStreamCommoditySources[i] = new();
					((IFixParser)NoUnderlyingDeliveryStreamCommoditySources[i]).Parse(viewNoUnderlyingDeliveryStreamCommoditySources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDeliveryStreamCommoditySources":
					value = NoUnderlyingDeliveryStreamCommoditySources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
