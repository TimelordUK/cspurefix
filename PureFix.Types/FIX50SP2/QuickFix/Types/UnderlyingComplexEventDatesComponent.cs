using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventDatesComponent : IFixComponent
	{
		[Group(NoOfTag = 2053, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventDates[]? NoUnderlyingComplexEventDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventDates is not null && NoUnderlyingComplexEventDates.Length != 0)
			{
				writer.WriteWholeNumber(2053, NoUnderlyingComplexEventDates.Length);
				for (int i = 0; i < NoUnderlyingComplexEventDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventDates") is IMessageView viewNoUnderlyingComplexEventDates)
			{
				var count = viewNoUnderlyingComplexEventDates.GroupCount();
				NoUnderlyingComplexEventDates = new NoUnderlyingComplexEventDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventDates[i] = new();
					((IFixParser)NoUnderlyingComplexEventDates[i]).Parse(viewNoUnderlyingComplexEventDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventDates":
					value = NoUnderlyingComplexEventDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventDates = null;
		}
	}
}
