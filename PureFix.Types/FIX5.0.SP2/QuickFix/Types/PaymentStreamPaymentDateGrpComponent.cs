using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamPaymentDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41220, Offset = 0, Required = false)]
		public NoPaymentStreamPaymentDates[]? NoPaymentStreamPaymentDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamPaymentDates is not null && NoPaymentStreamPaymentDates.Length != 0)
			{
				writer.WriteWholeNumber(41220, NoPaymentStreamPaymentDates.Length);
				for (int i = 0; i < NoPaymentStreamPaymentDates.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamPaymentDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamPaymentDates") is IMessageView viewNoPaymentStreamPaymentDates)
			{
				var count = viewNoPaymentStreamPaymentDates.GroupCount();
				NoPaymentStreamPaymentDates = new NoPaymentStreamPaymentDates[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamPaymentDates[i] = new();
					((IFixParser)NoPaymentStreamPaymentDates[i]).Parse(viewNoPaymentStreamPaymentDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamPaymentDates":
					value = NoPaymentStreamPaymentDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamPaymentDates = null;
		}
	}
}
