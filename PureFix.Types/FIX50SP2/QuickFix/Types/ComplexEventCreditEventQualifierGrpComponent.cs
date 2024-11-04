using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventCreditEventQualifierGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41005, Offset = 0, Required = false)]
		public NoComplexEventCreditEventQualifiers[]? NoComplexEventCreditEventQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventCreditEventQualifiers is not null && NoComplexEventCreditEventQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(41005, NoComplexEventCreditEventQualifiers.Length);
				for (int i = 0; i < NoComplexEventCreditEventQualifiers.Length; i++)
				{
					((IFixEncoder)NoComplexEventCreditEventQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventCreditEventQualifiers") is IMessageView viewNoComplexEventCreditEventQualifiers)
			{
				var count = viewNoComplexEventCreditEventQualifiers.GroupCount();
				NoComplexEventCreditEventQualifiers = new NoComplexEventCreditEventQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventCreditEventQualifiers[i] = new();
					((IFixParser)NoComplexEventCreditEventQualifiers[i]).Parse(viewNoComplexEventCreditEventQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventCreditEventQualifiers":
					value = NoComplexEventCreditEventQualifiers;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventCreditEventQualifiers = null;
		}
	}
}
