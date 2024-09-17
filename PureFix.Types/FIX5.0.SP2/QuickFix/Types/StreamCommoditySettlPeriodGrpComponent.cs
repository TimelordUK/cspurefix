using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCommoditySettlPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41289, Offset = 0, Required = false)]
		public NoStreamCommoditySettlPeriods[]? NoStreamCommoditySettlPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCommoditySettlPeriods is not null && NoStreamCommoditySettlPeriods.Length != 0)
			{
				writer.WriteWholeNumber(41289, NoStreamCommoditySettlPeriods.Length);
				for (int i = 0; i < NoStreamCommoditySettlPeriods.Length; i++)
				{
					((IFixEncoder)NoStreamCommoditySettlPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCommoditySettlPeriods") is IMessageView viewNoStreamCommoditySettlPeriods)
			{
				var count = viewNoStreamCommoditySettlPeriods.GroupCount();
				NoStreamCommoditySettlPeriods = new NoStreamCommoditySettlPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCommoditySettlPeriods[i] = new();
					((IFixParser)NoStreamCommoditySettlPeriods[i]).Parse(viewNoStreamCommoditySettlPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCommoditySettlPeriods":
					value = NoStreamCommoditySettlPeriods;
					break;
				default: return false;
			}
			return true;
		}
	}
}
