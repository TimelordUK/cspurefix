using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRateDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42709, Offset = 0, Required = false)]
		public NoReturnRateDates[]? NoReturnRateDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRateDates is not null && NoReturnRateDates.Length != 0)
			{
				writer.WriteWholeNumber(42709, NoReturnRateDates.Length);
				for (int i = 0; i < NoReturnRateDates.Length; i++)
				{
					((IFixEncoder)NoReturnRateDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRateDates") is IMessageView viewNoReturnRateDates)
			{
				var count = viewNoReturnRateDates.GroupCount();
				NoReturnRateDates = new NoReturnRateDates[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRateDates[i] = new();
					((IFixParser)NoReturnRateDates[i]).Parse(viewNoReturnRateDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRateDates":
					value = NoReturnRateDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
