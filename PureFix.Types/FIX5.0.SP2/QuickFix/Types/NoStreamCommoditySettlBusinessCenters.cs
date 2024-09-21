using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoStreamCommoditySettlBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41250, Type = TagType.String, Offset = 0, Required = false)]
		public string? StreamCommoditySettlBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StreamCommoditySettlBusinessCenter is not null) writer.WriteString(41250, StreamCommoditySettlBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			StreamCommoditySettlBusinessCenter = view.GetString(41250);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StreamCommoditySettlBusinessCenter":
					value = StreamCommoditySettlBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			StreamCommoditySettlBusinessCenter = null;
		}
	}
}
