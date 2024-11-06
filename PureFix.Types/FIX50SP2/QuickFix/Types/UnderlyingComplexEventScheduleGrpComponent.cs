using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventScheduleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41750, Offset = 0, Required = false)]
		public IOINoUnderlyingComplexEventSchedules[]? NoUnderlyingComplexEventSchedules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventSchedules is not null && NoUnderlyingComplexEventSchedules.Length != 0)
			{
				writer.WriteWholeNumber(41750, NoUnderlyingComplexEventSchedules.Length);
				for (int i = 0; i < NoUnderlyingComplexEventSchedules.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventSchedules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventSchedules") is IMessageView viewNoUnderlyingComplexEventSchedules)
			{
				var count = viewNoUnderlyingComplexEventSchedules.GroupCount();
				NoUnderlyingComplexEventSchedules = new IOINoUnderlyingComplexEventSchedules[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventSchedules[i] = new();
					((IFixParser)NoUnderlyingComplexEventSchedules[i]).Parse(viewNoUnderlyingComplexEventSchedules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventSchedules":
					value = NoUnderlyingComplexEventSchedules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventSchedules = null;
		}
	}
}
