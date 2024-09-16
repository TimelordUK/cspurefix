using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamFormulaMathGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42981, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamFormulas[]? NoUnderlyingPaymentStreamFormulas {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamFormulas is not null && NoUnderlyingPaymentStreamFormulas.Length != 0)
			{
				writer.WriteWholeNumber(42981, NoUnderlyingPaymentStreamFormulas.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamFormulas.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamFormulas[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamFormulas") is IMessageView viewNoUnderlyingPaymentStreamFormulas)
			{
				var count = viewNoUnderlyingPaymentStreamFormulas.GroupCount();
				NoUnderlyingPaymentStreamFormulas = new NoUnderlyingPaymentStreamFormulas[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamFormulas[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamFormulas[i]).Parse(viewNoUnderlyingPaymentStreamFormulas.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamFormulas":
					value = NoUnderlyingPaymentStreamFormulas;
					break;
				default: return false;
			}
			return true;
		}
	}
}
