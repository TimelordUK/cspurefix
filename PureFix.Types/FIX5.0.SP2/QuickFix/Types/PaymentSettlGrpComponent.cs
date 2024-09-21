using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentSettlGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40230, Offset = 0, Required = false)]
		public NoPaymentSettls[]? NoPaymentSettls {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentSettls is not null && NoPaymentSettls.Length != 0)
			{
				writer.WriteWholeNumber(40230, NoPaymentSettls.Length);
				for (int i = 0; i < NoPaymentSettls.Length; i++)
				{
					((IFixEncoder)NoPaymentSettls[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentSettls") is IMessageView viewNoPaymentSettls)
			{
				var count = viewNoPaymentSettls.GroupCount();
				NoPaymentSettls = new NoPaymentSettls[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentSettls[i] = new();
					((IFixParser)NoPaymentSettls[i]).Parse(viewNoPaymentSettls.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentSettls":
					value = NoPaymentSettls;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentSettls = null;
		}
	}
}
