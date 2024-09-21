using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCommoditySettlDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41283, Offset = 0, Required = false)]
		public NoStreamCommoditySettlDays[]? NoStreamCommoditySettlDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCommoditySettlDays is not null && NoStreamCommoditySettlDays.Length != 0)
			{
				writer.WriteWholeNumber(41283, NoStreamCommoditySettlDays.Length);
				for (int i = 0; i < NoStreamCommoditySettlDays.Length; i++)
				{
					((IFixEncoder)NoStreamCommoditySettlDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCommoditySettlDays") is IMessageView viewNoStreamCommoditySettlDays)
			{
				var count = viewNoStreamCommoditySettlDays.GroupCount();
				NoStreamCommoditySettlDays = new NoStreamCommoditySettlDays[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCommoditySettlDays[i] = new();
					((IFixParser)NoStreamCommoditySettlDays[i]).Parse(viewNoStreamCommoditySettlDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCommoditySettlDays":
					value = NoStreamCommoditySettlDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStreamCommoditySettlDays = null;
		}
	}
}
