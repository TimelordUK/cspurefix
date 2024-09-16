using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamFormulaMathGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42683, Offset = 0, Required = false)]
		public NoPaymentStreamFormulas[]? NoPaymentStreamFormulas {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamFormulas is not null && NoPaymentStreamFormulas.Length != 0)
			{
				writer.WriteWholeNumber(42683, NoPaymentStreamFormulas.Length);
				for (int i = 0; i < NoPaymentStreamFormulas.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamFormulas[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamFormulas") is IMessageView viewNoPaymentStreamFormulas)
			{
				var count = viewNoPaymentStreamFormulas.GroupCount();
				NoPaymentStreamFormulas = new NoPaymentStreamFormulas[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamFormulas[i] = new();
					((IFixParser)NoPaymentStreamFormulas[i]).Parse(viewNoPaymentStreamFormulas.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamFormulas":
					value = NoPaymentStreamFormulas;
					break;
				default: return false;
			}
			return true;
		}
	}
}
