using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPhysicalSettlDeliverableObligationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42065, Offset = 0, Required = false)]
		public NoUnderlyingPhysicalSettlDeliverableObligations[]? NoUnderlyingPhysicalSettlDeliverableObligations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPhysicalSettlDeliverableObligations is not null && NoUnderlyingPhysicalSettlDeliverableObligations.Length != 0)
			{
				writer.WriteWholeNumber(42065, NoUnderlyingPhysicalSettlDeliverableObligations.Length);
				for (int i = 0; i < NoUnderlyingPhysicalSettlDeliverableObligations.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPhysicalSettlDeliverableObligations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPhysicalSettlDeliverableObligations") is IMessageView viewNoUnderlyingPhysicalSettlDeliverableObligations)
			{
				var count = viewNoUnderlyingPhysicalSettlDeliverableObligations.GroupCount();
				NoUnderlyingPhysicalSettlDeliverableObligations = new NoUnderlyingPhysicalSettlDeliverableObligations[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPhysicalSettlDeliverableObligations[i] = new();
					((IFixParser)NoUnderlyingPhysicalSettlDeliverableObligations[i]).Parse(viewNoUnderlyingPhysicalSettlDeliverableObligations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPhysicalSettlDeliverableObligations":
					value = NoUnderlyingPhysicalSettlDeliverableObligations;
					break;
				default: return false;
			}
			return true;
		}
	}
}
