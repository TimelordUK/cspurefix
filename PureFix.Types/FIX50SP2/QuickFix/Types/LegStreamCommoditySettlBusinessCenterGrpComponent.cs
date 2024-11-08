using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCommoditySettlBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41646, Offset = 0, Required = false)]
		public IOINoLegStreamCommoditySettlBusinessCenters[]? NoLegStreamCommoditySettlBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCommoditySettlBusinessCenters is not null && NoLegStreamCommoditySettlBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41646, NoLegStreamCommoditySettlBusinessCenters.Length);
				for (int i = 0; i < NoLegStreamCommoditySettlBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegStreamCommoditySettlBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCommoditySettlBusinessCenters") is IMessageView viewNoLegStreamCommoditySettlBusinessCenters)
			{
				var count = viewNoLegStreamCommoditySettlBusinessCenters.GroupCount();
				NoLegStreamCommoditySettlBusinessCenters = new IOINoLegStreamCommoditySettlBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCommoditySettlBusinessCenters[i] = new();
					((IFixParser)NoLegStreamCommoditySettlBusinessCenters[i]).Parse(viewNoLegStreamCommoditySettlBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCommoditySettlBusinessCenters":
					value = NoLegStreamCommoditySettlBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCommoditySettlBusinessCenters = null;
		}
	}
}
