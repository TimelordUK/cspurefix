using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41031, Offset = 0, Required = false)]
		public NoComplexEventSchedules[]? NoComplexEventSchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventSchedules is not null && NoComplexEventSchedules.Length != 0)
			{
				writer.WriteWholeNumber(41031, NoComplexEventSchedules.Length);
				for (int i = 0; i < NoComplexEventSchedules.Length; i++)
				{
					((IFixEncoder)NoComplexEventSchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventSchedules") is IMessageView viewNoComplexEventSchedules)
			{
				var count = viewNoComplexEventSchedules.GroupCount();
				NoComplexEventSchedules = new NoComplexEventSchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventSchedules[i] = new();
					((IFixParser)NoComplexEventSchedules[i]).Parse(viewNoComplexEventSchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventSchedules":
					value = NoComplexEventSchedules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventSchedules = null;
		}
	}
}
