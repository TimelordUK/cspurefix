using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamFormulaMathGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42485, Offset = 0, Required = false)]
		public NoLegPaymentStreamFormulas[]? NoLegPaymentStreamFormulas {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamFormulas is not null && NoLegPaymentStreamFormulas.Length != 0)
			{
				writer.WriteWholeNumber(42485, NoLegPaymentStreamFormulas.Length);
				for (int i = 0; i < NoLegPaymentStreamFormulas.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamFormulas[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamFormulas") is IMessageView viewNoLegPaymentStreamFormulas)
			{
				var count = viewNoLegPaymentStreamFormulas.GroupCount();
				NoLegPaymentStreamFormulas = new NoLegPaymentStreamFormulas[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamFormulas[i] = new();
					((IFixParser)NoLegPaymentStreamFormulas[i]).Parse(viewNoLegPaymentStreamFormulas.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamFormulas":
					value = NoLegPaymentStreamFormulas;
					break;
				default: return false;
			}
			return true;
		}
	}
}
