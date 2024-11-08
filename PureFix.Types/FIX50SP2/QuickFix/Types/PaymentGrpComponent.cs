using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40212, Offset = 0, Required = false)]
		public ExecutionReportNoPayments[]? NoPayments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPayments is not null && NoPayments.Length != 0)
			{
				writer.WriteWholeNumber(40212, NoPayments.Length);
				for (int i = 0; i < NoPayments.Length; i++)
				{
					((IFixEncoder)NoPayments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPayments") is IMessageView viewNoPayments)
			{
				var count = viewNoPayments.GroupCount();
				NoPayments = new ExecutionReportNoPayments[count];
				for (int i = 0; i < count; i++)
				{
					NoPayments[i] = new();
					((IFixParser)NoPayments[i]).Parse(viewNoPayments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPayments":
					value = NoPayments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPayments = null;
		}
	}
}
