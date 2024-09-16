using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41400, Offset = 0, Required = false)]
		public NoLegComplexEventSchedules[]? NoLegComplexEventSchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventSchedules is not null && NoLegComplexEventSchedules.Length != 0)
			{
				writer.WriteWholeNumber(41400, NoLegComplexEventSchedules.Length);
				for (int i = 0; i < NoLegComplexEventSchedules.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventSchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventSchedules") is IMessageView viewNoLegComplexEventSchedules)
			{
				var count = viewNoLegComplexEventSchedules.GroupCount();
				NoLegComplexEventSchedules = new NoLegComplexEventSchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventSchedules[i] = new();
					((IFixParser)NoLegComplexEventSchedules[i]).Parse(viewNoLegComplexEventSchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventSchedules":
					value = NoLegComplexEventSchedules;
					break;
				default: return false;
			}
			return true;
		}
	}
}
