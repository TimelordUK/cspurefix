using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventDatesComponent : IFixComponent
	{
		[Group(NoOfTag = 2250, Offset = 0, Required = false)]
		public NoLegComplexEventDates[]? NoLegComplexEventDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventDates is not null && NoLegComplexEventDates.Length != 0)
			{
				writer.WriteWholeNumber(2250, NoLegComplexEventDates.Length);
				for (int i = 0; i < NoLegComplexEventDates.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventDates") is IMessageView viewNoLegComplexEventDates)
			{
				var count = viewNoLegComplexEventDates.GroupCount();
				NoLegComplexEventDates = new NoLegComplexEventDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventDates[i] = new();
					((IFixParser)NoLegComplexEventDates[i]).Parse(viewNoLegComplexEventDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventDates":
					value = NoLegComplexEventDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventDates = null;
		}
	}
}
