using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class CommissionDataGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2639, Offset = 0, Required = false)]
		public NoCommissions[]? NoCommissions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCommissions is not null && NoCommissions.Length != 0)
			{
				writer.WriteWholeNumber(2639, NoCommissions.Length);
				for (int i = 0; i < NoCommissions.Length; i++)
				{
					((IFixEncoder)NoCommissions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCommissions") is IMessageView viewNoCommissions)
			{
				var count = viewNoCommissions.GroupCount();
				NoCommissions = new NoCommissions[count];
				for (int i = 0; i < count; i++)
				{
					NoCommissions[i] = new();
					((IFixParser)NoCommissions[i]).Parse(viewNoCommissions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCommissions":
					value = NoCommissions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
