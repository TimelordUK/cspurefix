using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCommoditySettlBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41249, Offset = 0, Required = false)]
		public NoStreamCommoditySettlBusinessCenters[]? NoStreamCommoditySettlBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCommoditySettlBusinessCenters is not null && NoStreamCommoditySettlBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41249, NoStreamCommoditySettlBusinessCenters.Length);
				for (int i = 0; i < NoStreamCommoditySettlBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoStreamCommoditySettlBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCommoditySettlBusinessCenters") is IMessageView viewNoStreamCommoditySettlBusinessCenters)
			{
				var count = viewNoStreamCommoditySettlBusinessCenters.GroupCount();
				NoStreamCommoditySettlBusinessCenters = new NoStreamCommoditySettlBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCommoditySettlBusinessCenters[i] = new();
					((IFixParser)NoStreamCommoditySettlBusinessCenters[i]).Parse(viewNoStreamCommoditySettlBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCommoditySettlBusinessCenters":
					value = NoStreamCommoditySettlBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
