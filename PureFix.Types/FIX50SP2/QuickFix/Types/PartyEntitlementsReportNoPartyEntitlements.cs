using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyEntitlementsReportNoPartyEntitlements : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public PartyDetailGrpComponent? PartyDetailGrp {get; set;}
		
		[TagDetails(Tag = 1883, Type = TagType.Int, Offset = 1, Required = false)]
		public int? EntitlementStatus {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public EntitlementGrpComponent? EntitlementGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PartyDetailGrp is not null) ((IFixEncoder)PartyDetailGrp).Encode(writer);
			if (EntitlementStatus is not null) writer.WriteWholeNumber(1883, EntitlementStatus.Value);
			if (EntitlementGrp is not null) ((IFixEncoder)EntitlementGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
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
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PartyDetailGrp":
					value = PartyDetailGrp;
					break;
				case "EntitlementStatus":
					value = EntitlementStatus;
					break;
				case "EntitlementGrp":
					value = EntitlementGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)PartyDetailGrp)?.Reset();
			EntitlementStatus = null;
			((IFixReset?)EntitlementGrp)?.Reset();
		}
	}
}
