using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IndexRollMonthGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2734, Offset = 0, Required = false)]
		public NoIndexRollMonths[]? NoIndexRollMonths {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoIndexRollMonths is not null && NoIndexRollMonths.Length != 0)
			{
				writer.WriteWholeNumber(2734, NoIndexRollMonths.Length);
				for (int i = 0; i < NoIndexRollMonths.Length; i++)
				{
					((IFixEncoder)NoIndexRollMonths[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoIndexRollMonths") is IMessageView viewNoIndexRollMonths)
			{
				var count = viewNoIndexRollMonths.GroupCount();
				NoIndexRollMonths = new NoIndexRollMonths[count];
				for (int i = 0; i < count; i++)
				{
					NoIndexRollMonths[i] = new();
					((IFixParser)NoIndexRollMonths[i]).Parse(viewNoIndexRollMonths.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoIndexRollMonths":
					value = NoIndexRollMonths;
					break;
				default: return false;
			}
			return true;
		}
	}
}
