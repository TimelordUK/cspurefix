using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventCreditEventQualifierGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41374, Offset = 0, Required = false)]
		public NoLegComplexEventCreditEventQualifiers[]? NoLegComplexEventCreditEventQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventCreditEventQualifiers is not null && NoLegComplexEventCreditEventQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(41374, NoLegComplexEventCreditEventQualifiers.Length);
				for (int i = 0; i < NoLegComplexEventCreditEventQualifiers.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventCreditEventQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventCreditEventQualifiers") is IMessageView viewNoLegComplexEventCreditEventQualifiers)
			{
				var count = viewNoLegComplexEventCreditEventQualifiers.GroupCount();
				NoLegComplexEventCreditEventQualifiers = new NoLegComplexEventCreditEventQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventCreditEventQualifiers[i] = new();
					((IFixParser)NoLegComplexEventCreditEventQualifiers[i]).Parse(viewNoLegComplexEventCreditEventQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventCreditEventQualifiers":
					value = NoLegComplexEventCreditEventQualifiers;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventCreditEventQualifiers = null;
		}
	}
}
