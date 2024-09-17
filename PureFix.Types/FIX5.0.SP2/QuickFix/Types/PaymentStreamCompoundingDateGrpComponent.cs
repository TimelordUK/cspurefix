using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamCompoundingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42606, Offset = 0, Required = false)]
		public NoPaymentStreamCompoundingDates[]? NoPaymentStreamCompoundingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamCompoundingDates is not null && NoPaymentStreamCompoundingDates.Length != 0)
			{
				writer.WriteWholeNumber(42606, NoPaymentStreamCompoundingDates.Length);
				for (int i = 0; i < NoPaymentStreamCompoundingDates.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamCompoundingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamCompoundingDates") is IMessageView viewNoPaymentStreamCompoundingDates)
			{
				var count = viewNoPaymentStreamCompoundingDates.GroupCount();
				NoPaymentStreamCompoundingDates = new NoPaymentStreamCompoundingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamCompoundingDates[i] = new();
					((IFixParser)NoPaymentStreamCompoundingDates[i]).Parse(viewNoPaymentStreamCompoundingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamCompoundingDates":
					value = NoPaymentStreamCompoundingDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
