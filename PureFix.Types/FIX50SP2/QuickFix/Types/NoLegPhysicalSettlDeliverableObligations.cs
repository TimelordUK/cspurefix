using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPhysicalSettlDeliverableObligations : IFixGroup
	{
		[TagDetails(Tag = 41605, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegPhysicalSettlDeliverableObligationType {get; set;}
		
		[TagDetails(Tag = 41606, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegPhysicalSettlDeliverableObligationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPhysicalSettlDeliverableObligationType is not null) writer.WriteString(41605, LegPhysicalSettlDeliverableObligationType);
			if (LegPhysicalSettlDeliverableObligationValue is not null) writer.WriteString(41606, LegPhysicalSettlDeliverableObligationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPhysicalSettlDeliverableObligationType = view.GetString(41605);
			LegPhysicalSettlDeliverableObligationValue = view.GetString(41606);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPhysicalSettlDeliverableObligationType":
					value = LegPhysicalSettlDeliverableObligationType;
					break;
				case "LegPhysicalSettlDeliverableObligationValue":
					value = LegPhysicalSettlDeliverableObligationValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPhysicalSettlDeliverableObligationType = null;
			LegPhysicalSettlDeliverableObligationValue = null;
		}
	}
}
