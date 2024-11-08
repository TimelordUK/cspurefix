using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingDeliveryStreamCommoditySources : IFixGroup
	{
		[TagDetails(Tag = 41809, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingDeliveryStreamCommoditySource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDeliveryStreamCommoditySource is not null) writer.WriteString(41809, UnderlyingDeliveryStreamCommoditySource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDeliveryStreamCommoditySource = view.GetString(41809);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDeliveryStreamCommoditySource":
					value = UnderlyingDeliveryStreamCommoditySource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingDeliveryStreamCommoditySource = null;
		}
	}
}
