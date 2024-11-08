using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCommoditySettlTimeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41683, Offset = 0, Required = false)]
		public IOINoLegStreamCommoditySettlTimes[]? NoLegStreamCommoditySettlTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCommoditySettlTimes is not null && NoLegStreamCommoditySettlTimes.Length != 0)
			{
				writer.WriteWholeNumber(41683, NoLegStreamCommoditySettlTimes.Length);
				for (int i = 0; i < NoLegStreamCommoditySettlTimes.Length; i++)
				{
					((IFixEncoder)NoLegStreamCommoditySettlTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCommoditySettlTimes") is IMessageView viewNoLegStreamCommoditySettlTimes)
			{
				var count = viewNoLegStreamCommoditySettlTimes.GroupCount();
				NoLegStreamCommoditySettlTimes = new IOINoLegStreamCommoditySettlTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCommoditySettlTimes[i] = new();
					((IFixParser)NoLegStreamCommoditySettlTimes[i]).Parse(viewNoLegStreamCommoditySettlTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCommoditySettlTimes":
					value = NoLegStreamCommoditySettlTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCommoditySettlTimes = null;
		}
	}
}
