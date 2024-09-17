using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoAdditionalTerms : IFixGroup
	{
		[TagDetails(Tag = 40020, Type = TagType.Boolean, Offset = 0, Required = false)]
		public bool? AdditionalTermConditionPrecedentBondIndicator {get; set;}
		
		[TagDetails(Tag = 40021, Type = TagType.Boolean, Offset = 1, Required = false)]
		public bool? AdditionalTermDiscrepancyClauseIndicator {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public AdditionalTermBondRefGrpComponent? AdditionalTermBondRefGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AdditionalTermConditionPrecedentBondIndicator is not null) writer.WriteBoolean(40020, AdditionalTermConditionPrecedentBondIndicator.Value);
			if (AdditionalTermDiscrepancyClauseIndicator is not null) writer.WriteBoolean(40021, AdditionalTermDiscrepancyClauseIndicator.Value);
			if (AdditionalTermBondRefGrp is not null) ((IFixEncoder)AdditionalTermBondRefGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AdditionalTermConditionPrecedentBondIndicator = view.GetBool(40020);
			AdditionalTermDiscrepancyClauseIndicator = view.GetBool(40021);
			if (view.GetView("AdditionalTermBondRefGrp") is IMessageView viewAdditionalTermBondRefGrp)
			{
				AdditionalTermBondRefGrp = new();
				((IFixParser)AdditionalTermBondRefGrp).Parse(viewAdditionalTermBondRefGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AdditionalTermConditionPrecedentBondIndicator":
					value = AdditionalTermConditionPrecedentBondIndicator;
					break;
				case "AdditionalTermDiscrepancyClauseIndicator":
					value = AdditionalTermDiscrepancyClauseIndicator;
					break;
				case "AdditionalTermBondRefGrp":
					value = AdditionalTermBondRefGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
