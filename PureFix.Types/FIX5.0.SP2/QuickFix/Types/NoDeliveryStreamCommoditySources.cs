using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoDeliveryStreamCommoditySources : IFixGroup
	{
		[TagDetails(Tag = 41086, Type = TagType.String, Offset = 0, Required = false)]
		public string? DeliveryStreamCommoditySource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DeliveryStreamCommoditySource is not null) writer.WriteString(41086, DeliveryStreamCommoditySource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DeliveryStreamCommoditySource = view.GetString(41086);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DeliveryStreamCommoditySource":
					value = DeliveryStreamCommoditySource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DeliveryStreamCommoditySource = null;
		}
	}
}
