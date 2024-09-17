using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamNonDeliverableFixingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40825, Offset = 0, Required = false)]
		public NoNonDeliverableFixingDates[]? NoNonDeliverableFixingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNonDeliverableFixingDates is not null && NoNonDeliverableFixingDates.Length != 0)
			{
				writer.WriteWholeNumber(40825, NoNonDeliverableFixingDates.Length);
				for (int i = 0; i < NoNonDeliverableFixingDates.Length; i++)
				{
					((IFixEncoder)NoNonDeliverableFixingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNonDeliverableFixingDates") is IMessageView viewNoNonDeliverableFixingDates)
			{
				var count = viewNoNonDeliverableFixingDates.GroupCount();
				NoNonDeliverableFixingDates = new NoNonDeliverableFixingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoNonDeliverableFixingDates[i] = new();
					((IFixParser)NoNonDeliverableFixingDates[i]).Parse(viewNoNonDeliverableFixingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNonDeliverableFixingDates":
					value = NoNonDeliverableFixingDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
