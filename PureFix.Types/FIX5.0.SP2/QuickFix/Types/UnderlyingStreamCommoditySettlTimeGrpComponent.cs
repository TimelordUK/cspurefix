using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCommoditySettlTimeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41999, Offset = 0, Required = false)]
		public NoUnderlyingStreamCommoditySettlTimes[]? NoUnderlyingStreamCommoditySettlTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCommoditySettlTimes is not null && NoUnderlyingStreamCommoditySettlTimes.Length != 0)
			{
				writer.WriteWholeNumber(41999, NoUnderlyingStreamCommoditySettlTimes.Length);
				for (int i = 0; i < NoUnderlyingStreamCommoditySettlTimes.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCommoditySettlTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCommoditySettlTimes") is IMessageView viewNoUnderlyingStreamCommoditySettlTimes)
			{
				var count = viewNoUnderlyingStreamCommoditySettlTimes.GroupCount();
				NoUnderlyingStreamCommoditySettlTimes = new NoUnderlyingStreamCommoditySettlTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCommoditySettlTimes[i] = new();
					((IFixParser)NoUnderlyingStreamCommoditySettlTimes[i]).Parse(viewNoUnderlyingStreamCommoditySettlTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCommoditySettlTimes":
					value = NoUnderlyingStreamCommoditySettlTimes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
