using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegReturnRateDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42508, Offset = 0, Required = false)]
		public NoLegReturnRateDates[]? NoLegReturnRateDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegReturnRateDates is not null && NoLegReturnRateDates.Length != 0)
			{
				writer.WriteWholeNumber(42508, NoLegReturnRateDates.Length);
				for (int i = 0; i < NoLegReturnRateDates.Length; i++)
				{
					((IFixEncoder)NoLegReturnRateDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegReturnRateDates") is IMessageView viewNoLegReturnRateDates)
			{
				var count = viewNoLegReturnRateDates.GroupCount();
				NoLegReturnRateDates = new NoLegReturnRateDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegReturnRateDates[i] = new();
					((IFixParser)NoLegReturnRateDates[i]).Parse(viewNoLegReturnRateDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegReturnRateDates":
					value = NoLegReturnRateDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
