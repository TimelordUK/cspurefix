using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DeliveryStreamCommoditySourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41085, Offset = 0, Required = false)]
		public NoDeliveryStreamCommoditySources[]? NoDeliveryStreamCommoditySources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDeliveryStreamCommoditySources is not null && NoDeliveryStreamCommoditySources.Length != 0)
			{
				writer.WriteWholeNumber(41085, NoDeliveryStreamCommoditySources.Length);
				for (int i = 0; i < NoDeliveryStreamCommoditySources.Length; i++)
				{
					((IFixEncoder)NoDeliveryStreamCommoditySources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDeliveryStreamCommoditySources") is IMessageView viewNoDeliveryStreamCommoditySources)
			{
				var count = viewNoDeliveryStreamCommoditySources.GroupCount();
				NoDeliveryStreamCommoditySources = new NoDeliveryStreamCommoditySources[count];
				for (int i = 0; i < count; i++)
				{
					NoDeliveryStreamCommoditySources[i] = new();
					((IFixParser)NoDeliveryStreamCommoditySources[i]).Parse(viewNoDeliveryStreamCommoditySources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDeliveryStreamCommoditySources":
					value = NoDeliveryStreamCommoditySources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
