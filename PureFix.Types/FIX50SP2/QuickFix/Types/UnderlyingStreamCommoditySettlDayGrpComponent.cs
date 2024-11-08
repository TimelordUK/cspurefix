using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCommoditySettlDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41996, Offset = 0, Required = false)]
		public IOINoUnderlyingStreamCommoditySettlDays[]? NoUnderlyingStreamCommoditySettlDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCommoditySettlDays is not null && NoUnderlyingStreamCommoditySettlDays.Length != 0)
			{
				writer.WriteWholeNumber(41996, NoUnderlyingStreamCommoditySettlDays.Length);
				for (int i = 0; i < NoUnderlyingStreamCommoditySettlDays.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCommoditySettlDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCommoditySettlDays") is IMessageView viewNoUnderlyingStreamCommoditySettlDays)
			{
				var count = viewNoUnderlyingStreamCommoditySettlDays.GroupCount();
				NoUnderlyingStreamCommoditySettlDays = new IOINoUnderlyingStreamCommoditySettlDays[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCommoditySettlDays[i] = new();
					((IFixParser)NoUnderlyingStreamCommoditySettlDays[i]).Parse(viewNoUnderlyingStreamCommoditySettlDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCommoditySettlDays":
					value = NoUnderlyingStreamCommoditySettlDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamCommoditySettlDays = null;
		}
	}
}
