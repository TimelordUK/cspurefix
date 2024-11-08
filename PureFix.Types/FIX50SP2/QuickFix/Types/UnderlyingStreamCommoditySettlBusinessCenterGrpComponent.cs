using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCommoditySettlBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41962, Offset = 0, Required = false)]
		public IOINoUnderlyingStreamCommoditySettlBusinessCenters[]? NoUnderlyingStreamCommoditySettlBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCommoditySettlBusinessCenters is not null && NoUnderlyingStreamCommoditySettlBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41962, NoUnderlyingStreamCommoditySettlBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingStreamCommoditySettlBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCommoditySettlBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCommoditySettlBusinessCenters") is IMessageView viewNoUnderlyingStreamCommoditySettlBusinessCenters)
			{
				var count = viewNoUnderlyingStreamCommoditySettlBusinessCenters.GroupCount();
				NoUnderlyingStreamCommoditySettlBusinessCenters = new IOINoUnderlyingStreamCommoditySettlBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCommoditySettlBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingStreamCommoditySettlBusinessCenters[i]).Parse(viewNoUnderlyingStreamCommoditySettlBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCommoditySettlBusinessCenters":
					value = NoUnderlyingStreamCommoditySettlBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamCommoditySettlBusinessCenters = null;
		}
	}
}
