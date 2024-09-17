using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCommoditySettlTimeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41286, Offset = 0, Required = false)]
		public NoStreamCommoditySettlTimes[]? NoStreamCommoditySettlTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCommoditySettlTimes is not null && NoStreamCommoditySettlTimes.Length != 0)
			{
				writer.WriteWholeNumber(41286, NoStreamCommoditySettlTimes.Length);
				for (int i = 0; i < NoStreamCommoditySettlTimes.Length; i++)
				{
					((IFixEncoder)NoStreamCommoditySettlTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCommoditySettlTimes") is IMessageView viewNoStreamCommoditySettlTimes)
			{
				var count = viewNoStreamCommoditySettlTimes.GroupCount();
				NoStreamCommoditySettlTimes = new NoStreamCommoditySettlTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCommoditySettlTimes[i] = new();
					((IFixParser)NoStreamCommoditySettlTimes[i]).Parse(viewNoStreamCommoditySettlTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCommoditySettlTimes":
					value = NoStreamCommoditySettlTimes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
