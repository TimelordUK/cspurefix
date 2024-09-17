using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventCreditEventQualifierGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41724, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventCreditEventQualifiers[]? NoUnderlyingComplexEventCreditEventQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventCreditEventQualifiers is not null && NoUnderlyingComplexEventCreditEventQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(41724, NoUnderlyingComplexEventCreditEventQualifiers.Length);
				for (int i = 0; i < NoUnderlyingComplexEventCreditEventQualifiers.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventCreditEventQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventCreditEventQualifiers") is IMessageView viewNoUnderlyingComplexEventCreditEventQualifiers)
			{
				var count = viewNoUnderlyingComplexEventCreditEventQualifiers.GroupCount();
				NoUnderlyingComplexEventCreditEventQualifiers = new NoUnderlyingComplexEventCreditEventQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventCreditEventQualifiers[i] = new();
					((IFixParser)NoUnderlyingComplexEventCreditEventQualifiers[i]).Parse(viewNoUnderlyingComplexEventCreditEventQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventCreditEventQualifiers":
					value = NoUnderlyingComplexEventCreditEventQualifiers;
					break;
				default: return false;
			}
			return true;
		}
	}
}
