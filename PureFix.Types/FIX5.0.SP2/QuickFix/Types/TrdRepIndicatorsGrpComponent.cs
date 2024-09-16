using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TrdRepIndicatorsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1387, Offset = 0, Required = false)]
		public NoTrdRepIndicators[]? NoTrdRepIndicators {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrdRepIndicators is not null && NoTrdRepIndicators.Length != 0)
			{
				writer.WriteWholeNumber(1387, NoTrdRepIndicators.Length);
				for (int i = 0; i < NoTrdRepIndicators.Length; i++)
				{
					((IFixEncoder)NoTrdRepIndicators[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTrdRepIndicators") is IMessageView viewNoTrdRepIndicators)
			{
				var count = viewNoTrdRepIndicators.GroupCount();
				NoTrdRepIndicators = new NoTrdRepIndicators[count];
				for (int i = 0; i < count; i++)
				{
					NoTrdRepIndicators[i] = new();
					((IFixParser)NoTrdRepIndicators[i]).Parse(viewNoTrdRepIndicators.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTrdRepIndicators":
					value = NoTrdRepIndicators;
					break;
				default: return false;
			}
			return true;
		}
	}
}
