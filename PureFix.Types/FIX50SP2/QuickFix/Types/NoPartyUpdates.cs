using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPartyUpdates : IFixGroup
	{
		[TagDetails(Tag = 1324, Type = TagType.String, Offset = 0, Required = false)]
		public string? ListUpdateAction {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public PartyDetailGrpComponent? PartyDetailGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ListUpdateAction is not null) writer.WriteString(1324, ListUpdateAction);
			if (PartyDetailGrp is not null) ((IFixEncoder)PartyDetailGrp).Encode(writer);
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
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ListUpdateAction = null;
			((IFixReset?)PartyDetailGrp)?.Reset();
		}
	}
}
