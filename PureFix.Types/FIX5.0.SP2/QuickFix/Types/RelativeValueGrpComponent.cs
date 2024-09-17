using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelativeValueGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2529, Offset = 0, Required = false)]
		public NoRelativeValues[]? NoRelativeValues {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelativeValues is not null && NoRelativeValues.Length != 0)
			{
				writer.WriteWholeNumber(2529, NoRelativeValues.Length);
				for (int i = 0; i < NoRelativeValues.Length; i++)
				{
					((IFixEncoder)NoRelativeValues[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelativeValues") is IMessageView viewNoRelativeValues)
			{
				var count = viewNoRelativeValues.GroupCount();
				NoRelativeValues = new NoRelativeValues[count];
				for (int i = 0; i < count; i++)
				{
					NoRelativeValues[i] = new();
					((IFixParser)NoRelativeValues[i]).Parse(viewNoRelativeValues.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelativeValues":
					value = NoRelativeValues;
					break;
				default: return false;
			}
			return true;
		}
	}
}
