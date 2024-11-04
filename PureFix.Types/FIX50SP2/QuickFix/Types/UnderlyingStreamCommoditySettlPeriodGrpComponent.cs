using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCommoditySettlPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42002, Offset = 0, Required = false)]
		public NoUnderlyingStreamCommoditySettlPeriods[]? NoUnderlyingStreamCommoditySettlPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCommoditySettlPeriods is not null && NoUnderlyingStreamCommoditySettlPeriods.Length != 0)
			{
				writer.WriteWholeNumber(42002, NoUnderlyingStreamCommoditySettlPeriods.Length);
				for (int i = 0; i < NoUnderlyingStreamCommoditySettlPeriods.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCommoditySettlPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCommoditySettlPeriods") is IMessageView viewNoUnderlyingStreamCommoditySettlPeriods)
			{
				var count = viewNoUnderlyingStreamCommoditySettlPeriods.GroupCount();
				NoUnderlyingStreamCommoditySettlPeriods = new NoUnderlyingStreamCommoditySettlPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCommoditySettlPeriods[i] = new();
					((IFixParser)NoUnderlyingStreamCommoditySettlPeriods[i]).Parse(viewNoUnderlyingStreamCommoditySettlPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCommoditySettlPeriods":
					value = NoUnderlyingStreamCommoditySettlPeriods;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamCommoditySettlPeriods = null;
		}
	}
}
