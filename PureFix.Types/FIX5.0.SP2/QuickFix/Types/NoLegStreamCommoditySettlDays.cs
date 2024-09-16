using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegStreamCommoditySettlDays : IFixGroup
	{
		[TagDetails(Tag = 41681, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegStreamCommoditySettlDay {get; set;}
		
		[TagDetails(Tag = 41682, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegStreamCommoditySettlTotalHours {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public LegStreamCommoditySettlTimeGrpComponent? LegStreamCommoditySettlTimeGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegStreamCommoditySettlDay is not null) writer.WriteWholeNumber(41681, LegStreamCommoditySettlDay.Value);
			if (LegStreamCommoditySettlTotalHours is not null) writer.WriteWholeNumber(41682, LegStreamCommoditySettlTotalHours.Value);
			if (LegStreamCommoditySettlTimeGrp is not null) ((IFixEncoder)LegStreamCommoditySettlTimeGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegStreamCommoditySettlDay = view.GetInt32(41681);
			LegStreamCommoditySettlTotalHours = view.GetInt32(41682);
			if (view.GetView("LegStreamCommoditySettlTimeGrp") is IMessageView viewLegStreamCommoditySettlTimeGrp)
			{
				LegStreamCommoditySettlTimeGrp = new();
				((IFixParser)LegStreamCommoditySettlTimeGrp).Parse(viewLegStreamCommoditySettlTimeGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegStreamCommoditySettlDay":
					value = LegStreamCommoditySettlDay;
					break;
				case "LegStreamCommoditySettlTotalHours":
					value = LegStreamCommoditySettlTotalHours;
					break;
				case "LegStreamCommoditySettlTimeGrp":
					value = LegStreamCommoditySettlTimeGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
