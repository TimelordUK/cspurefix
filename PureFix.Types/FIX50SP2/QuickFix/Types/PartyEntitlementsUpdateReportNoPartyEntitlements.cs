using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyEntitlementsUpdateReportNoPartyEntitlements : IFixGroup
	{
		[TagDetails(Tag = 1324, Type = TagType.String, Offset = 0, Required = false)]
		public string? ListUpdateAction {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public PartyDetailGrpComponent? PartyDetailGrp {get; set;}
		
		[TagDetails(Tag = 1883, Type = TagType.Int, Offset = 2, Required = false)]
		public int? EntitlementStatus {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public EntitlementGrpComponent? EntitlementGrp {get; set;}
		
		[TagDetails(Tag = 1885, Type = TagType.String, Offset = 4, Required = false)]
		public string? EntitlementRefID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ListUpdateAction is not null) writer.WriteString(1324, ListUpdateAction);
			if (PartyDetailGrp is not null) ((IFixEncoder)PartyDetailGrp).Encode(writer);
			if (EntitlementStatus is not null) writer.WriteWholeNumber(1883, EntitlementStatus.Value);
			if (EntitlementGrp is not null) ((IFixEncoder)EntitlementGrp).Encode(writer);
			if (EntitlementRefID is not null) writer.WriteString(1885, EntitlementRefID);
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
			EntitlementStatus = view.GetInt32(1883);
			if (view.GetView("EntitlementGrp") is IMessageView viewEntitlementGrp)
			{
				EntitlementGrp = new();
				((IFixParser)EntitlementGrp).Parse(viewEntitlementGrp);
			}
			EntitlementRefID = view.GetString(1885);
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
				case "EntitlementStatus":
					value = EntitlementStatus;
					break;
				case "EntitlementGrp":
					value = EntitlementGrp;
					break;
				case "EntitlementRefID":
					value = EntitlementRefID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ListUpdateAction = null;
			((IFixReset?)PartyDetailGrp)?.Reset();
			EntitlementStatus = null;
			((IFixReset?)EntitlementGrp)?.Reset();
			EntitlementRefID = null;
		}
	}
}
