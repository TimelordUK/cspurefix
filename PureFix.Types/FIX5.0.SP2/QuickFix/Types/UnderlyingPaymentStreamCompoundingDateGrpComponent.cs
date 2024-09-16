using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamCompoundingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42901, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamCompoundingDates[]? NoUnderlyingPaymentStreamCompoundingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamCompoundingDates is not null && NoUnderlyingPaymentStreamCompoundingDates.Length != 0)
			{
				writer.WriteWholeNumber(42901, NoUnderlyingPaymentStreamCompoundingDates.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamCompoundingDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamCompoundingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamCompoundingDates") is IMessageView viewNoUnderlyingPaymentStreamCompoundingDates)
			{
				var count = viewNoUnderlyingPaymentStreamCompoundingDates.GroupCount();
				NoUnderlyingPaymentStreamCompoundingDates = new NoUnderlyingPaymentStreamCompoundingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamCompoundingDates[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamCompoundingDates[i]).Parse(viewNoUnderlyingPaymentStreamCompoundingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamCompoundingDates":
					value = NoUnderlyingPaymentStreamCompoundingDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
