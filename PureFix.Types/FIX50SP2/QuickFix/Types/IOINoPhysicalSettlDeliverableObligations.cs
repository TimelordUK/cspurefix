using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoPhysicalSettlDeliverableObligations : IFixGroup
	{
		[TagDetails(Tag = 40210, Type = TagType.String, Offset = 0, Required = false)]
		public string? PhysicalSettlDeliverableObligationType {get; set;}
		
		[TagDetails(Tag = 40211, Type = TagType.String, Offset = 1, Required = false)]
		public string? PhysicalSettlDeliverableObligationValue {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PhysicalSettlDeliverableObligationType is not null) writer.WriteString(40210, PhysicalSettlDeliverableObligationType);
			if (PhysicalSettlDeliverableObligationValue is not null) writer.WriteString(40211, PhysicalSettlDeliverableObligationValue);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PhysicalSettlDeliverableObligationType = view.GetString(40210);
			PhysicalSettlDeliverableObligationValue = view.GetString(40211);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PhysicalSettlDeliverableObligationType":
					value = PhysicalSettlDeliverableObligationType;
					break;
				case "PhysicalSettlDeliverableObligationValue":
					value = PhysicalSettlDeliverableObligationValue;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PhysicalSettlDeliverableObligationType = null;
			PhysicalSettlDeliverableObligationValue = null;
		}
	}
}
