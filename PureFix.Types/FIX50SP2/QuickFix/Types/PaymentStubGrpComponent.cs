using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40872, Offset = 0, Required = false)]
		public IOINoPaymentStubs[]? NoPaymentStubs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStubs is not null && NoPaymentStubs.Length != 0)
			{
				writer.WriteWholeNumber(40872, NoPaymentStubs.Length);
				for (int i = 0; i < NoPaymentStubs.Length; i++)
				{
					((IFixEncoder)NoPaymentStubs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStubs") is IMessageView viewNoPaymentStubs)
			{
				var count = viewNoPaymentStubs.GroupCount();
				NoPaymentStubs = new IOINoPaymentStubs[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStubs[i] = new();
					((IFixParser)NoPaymentStubs[i]).Parse(viewNoPaymentStubs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStubs":
					value = NoPaymentStubs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStubs = null;
		}
	}
}
