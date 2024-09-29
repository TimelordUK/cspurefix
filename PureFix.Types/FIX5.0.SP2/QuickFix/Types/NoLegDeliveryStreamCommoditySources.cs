using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegDeliveryStreamCommoditySources : IFixGroup
	{
		[TagDetails(Tag = 41461, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegDeliveryStreamCommoditySource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegDeliveryStreamCommoditySource is not null) writer.WriteString(41461, LegDeliveryStreamCommoditySource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegDeliveryStreamCommoditySource = view.GetString(41461);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegDeliveryStreamCommoditySource":
					value = LegDeliveryStreamCommoditySource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegDeliveryStreamCommoditySource = null;
		}
	}
}
