using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCommoditySettlDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41680, Offset = 0, Required = false)]
		public IOINoLegStreamCommoditySettlDays[]? NoLegStreamCommoditySettlDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCommoditySettlDays is not null && NoLegStreamCommoditySettlDays.Length != 0)
			{
				writer.WriteWholeNumber(41680, NoLegStreamCommoditySettlDays.Length);
				for (int i = 0; i < NoLegStreamCommoditySettlDays.Length; i++)
				{
					((IFixEncoder)NoLegStreamCommoditySettlDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCommoditySettlDays") is IMessageView viewNoLegStreamCommoditySettlDays)
			{
				var count = viewNoLegStreamCommoditySettlDays.GroupCount();
				NoLegStreamCommoditySettlDays = new IOINoLegStreamCommoditySettlDays[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCommoditySettlDays[i] = new();
					((IFixParser)NoLegStreamCommoditySettlDays[i]).Parse(viewNoLegStreamCommoditySettlDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCommoditySettlDays":
					value = NoLegStreamCommoditySettlDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCommoditySettlDays = null;
		}
	}
}
