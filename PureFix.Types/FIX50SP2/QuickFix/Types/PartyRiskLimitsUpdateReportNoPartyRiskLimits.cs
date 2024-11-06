using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyRiskLimitsUpdateReportNoPartyRiskLimits : IFixGroup
	{
		[TagDetails(Tag = 1324, Type = TagType.String, Offset = 0, Required = false)]
		public string? ListUpdateAction {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public PartyDetailGrpComponent? PartyDetailGrp {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public RiskLimitsGrpComponent? RiskLimitsGrp {get; set;}
		
		[TagDetails(Tag = 1670, Type = TagType.String, Offset = 3, Required = false)]
		public string? RiskLimitID {get; set;}
		
		[TagDetails(Tag = 2339, Type = TagType.Int, Offset = 4, Required = false)]
		public int? RiskLimitCheckModelType {get; set;}
		
		[TagDetails(Tag = 2355, Type = TagType.Int, Offset = 5, Required = false)]
		public int? PartyRiskLimitStatus {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ListUpdateAction is not null) writer.WriteString(1324, ListUpdateAction);
			if (PartyDetailGrp is not null) ((IFixEncoder)PartyDetailGrp).Encode(writer);
			if (RiskLimitsGrp is not null) ((IFixEncoder)RiskLimitsGrp).Encode(writer);
			if (RiskLimitID is not null) writer.WriteString(1670, RiskLimitID);
			if (RiskLimitCheckModelType is not null) writer.WriteWholeNumber(2339, RiskLimitCheckModelType.Value);
			if (PartyRiskLimitStatus is not null) writer.WriteWholeNumber(2355, PartyRiskLimitStatus.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ListUpdateAction = view.GetString(1324);
			if (view.GetView("PartyDetailGrp") is IMessageView viewPartyDetailGrp)
			{
				PartyDetailGrp = new();
				((IFixParser)PartyDetailGrp).Parse(viewPartyDetailGrp);
			}
			if (view.GetView("RiskLimitsGrp") is IMessageView viewRiskLimitsGrp)
			{
				RiskLimitsGrp = new();
				((IFixParser)RiskLimitsGrp).Parse(viewRiskLimitsGrp);
			}
			RiskLimitID = view.GetString(1670);
			RiskLimitCheckModelType = view.GetInt32(2339);
			PartyRiskLimitStatus = view.GetInt32(2355);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ListUpdateAction":
					value = ListUpdateAction;
					break;
				case "PartyDetailGrp":
					value = PartyDetailGrp;
					break;
				case "RiskLimitsGrp":
					value = RiskLimitsGrp;
					break;
				case "RiskLimitID":
					value = RiskLimitID;
					break;
				case "RiskLimitCheckModelType":
					value = RiskLimitCheckModelType;
					break;
				case "PartyRiskLimitStatus":
					value = PartyRiskLimitStatus;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ListUpdateAction = null;
			((IFixReset?)PartyDetailGrp)?.Reset();
			((IFixReset?)RiskLimitsGrp)?.Reset();
			RiskLimitID = null;
			RiskLimitCheckModelType = null;
			PartyRiskLimitStatus = null;
		}
	}
}
