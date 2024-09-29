using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCommoditySettlPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41686, Offset = 0, Required = false)]
		public NoLegStreamCommoditySettlPeriods[]? NoLegStreamCommoditySettlPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCommoditySettlPeriods is not null && NoLegStreamCommoditySettlPeriods.Length != 0)
			{
				writer.WriteWholeNumber(41686, NoLegStreamCommoditySettlPeriods.Length);
				for (int i = 0; i < NoLegStreamCommoditySettlPeriods.Length; i++)
				{
					((IFixEncoder)NoLegStreamCommoditySettlPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCommoditySettlPeriods") is IMessageView viewNoLegStreamCommoditySettlPeriods)
			{
				var count = viewNoLegStreamCommoditySettlPeriods.GroupCount();
				NoLegStreamCommoditySettlPeriods = new NoLegStreamCommoditySettlPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCommoditySettlPeriods[i] = new();
					((IFixParser)NoLegStreamCommoditySettlPeriods[i]).Parse(viewNoLegStreamCommoditySettlPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCommoditySettlPeriods":
					value = NoLegStreamCommoditySettlPeriods;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCommoditySettlPeriods = null;
		}
	}
}
