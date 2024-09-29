using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40828, Offset = 0, Required = false)]
		public NoPaymentSchedules[]? NoPaymentSchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentSchedules is not null && NoPaymentSchedules.Length != 0)
			{
				writer.WriteWholeNumber(40828, NoPaymentSchedules.Length);
				for (int i = 0; i < NoPaymentSchedules.Length; i++)
				{
					((IFixEncoder)NoPaymentSchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentSchedules") is IMessageView viewNoPaymentSchedules)
			{
				var count = viewNoPaymentSchedules.GroupCount();
				NoPaymentSchedules = new NoPaymentSchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentSchedules[i] = new();
					((IFixParser)NoPaymentSchedules[i]).Parse(viewNoPaymentSchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentSchedules":
					value = NoPaymentSchedules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentSchedules = null;
		}
	}
}
