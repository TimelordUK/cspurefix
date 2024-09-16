using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40664, Offset = 0, Required = false)]
		public NoUnderlyingPaymentSchedules[]? NoUnderlyingPaymentSchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentSchedules is not null && NoUnderlyingPaymentSchedules.Length != 0)
			{
				writer.WriteWholeNumber(40664, NoUnderlyingPaymentSchedules.Length);
				for (int i = 0; i < NoUnderlyingPaymentSchedules.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentSchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentSchedules") is IMessageView viewNoUnderlyingPaymentSchedules)
			{
				var count = viewNoUnderlyingPaymentSchedules.GroupCount();
				NoUnderlyingPaymentSchedules = new NoUnderlyingPaymentSchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentSchedules[i] = new();
					((IFixParser)NoUnderlyingPaymentSchedules[i]).Parse(viewNoUnderlyingPaymentSchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentSchedules":
					value = NoUnderlyingPaymentSchedules;
					break;
				default: return false;
			}
			return true;
		}
	}
}
