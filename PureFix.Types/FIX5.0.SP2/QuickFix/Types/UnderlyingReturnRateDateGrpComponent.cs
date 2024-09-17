using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRateDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43008, Offset = 0, Required = false)]
		public NoUnderlyingReturnRateDates[]? NoUnderlyingReturnRateDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRateDates is not null && NoUnderlyingReturnRateDates.Length != 0)
			{
				writer.WriteWholeNumber(43008, NoUnderlyingReturnRateDates.Length);
				for (int i = 0; i < NoUnderlyingReturnRateDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRateDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRateDates") is IMessageView viewNoUnderlyingReturnRateDates)
			{
				var count = viewNoUnderlyingReturnRateDates.GroupCount();
				NoUnderlyingReturnRateDates = new NoUnderlyingReturnRateDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRateDates[i] = new();
					((IFixParser)NoUnderlyingReturnRateDates[i]).Parse(viewNoUnderlyingReturnRateDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRateDates":
					value = NoUnderlyingReturnRateDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
