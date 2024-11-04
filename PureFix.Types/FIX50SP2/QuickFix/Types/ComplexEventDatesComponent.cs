using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventDatesComponent : IFixComponent
	{
		[Group(NoOfTag = 1491, Offset = 0, Required = false)]
		public NoComplexEventDates[]? NoComplexEventDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventDates is not null && NoComplexEventDates.Length != 0)
			{
				writer.WriteWholeNumber(1491, NoComplexEventDates.Length);
				for (int i = 0; i < NoComplexEventDates.Length; i++)
				{
					((IFixEncoder)NoComplexEventDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventDates") is IMessageView viewNoComplexEventDates)
			{
				var count = viewNoComplexEventDates.GroupCount();
				NoComplexEventDates = new NoComplexEventDates[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventDates[i] = new();
					((IFixParser)NoComplexEventDates[i]).Parse(viewNoComplexEventDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventDates":
					value = NoComplexEventDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventDates = null;
		}
	}
}
