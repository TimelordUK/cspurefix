using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41010, Offset = 0, Required = false)]
		public IOINoComplexEventPeriods[]? NoComplexEventPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventPeriods is not null && NoComplexEventPeriods.Length != 0)
			{
				writer.WriteWholeNumber(41010, NoComplexEventPeriods.Length);
				for (int i = 0; i < NoComplexEventPeriods.Length; i++)
				{
					((IFixEncoder)NoComplexEventPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventPeriods") is IMessageView viewNoComplexEventPeriods)
			{
				var count = viewNoComplexEventPeriods.GroupCount();
				NoComplexEventPeriods = new IOINoComplexEventPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventPeriods[i] = new();
					((IFixParser)NoComplexEventPeriods[i]).Parse(viewNoComplexEventPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventPeriods":
					value = NoComplexEventPeriods;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventPeriods = null;
		}
	}
}
