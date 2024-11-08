using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MatchExceptionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2772, Offset = 0, Required = false)]
		public ConfirmationNoMatchExceptions[]? NoMatchExceptions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMatchExceptions is not null && NoMatchExceptions.Length != 0)
			{
				writer.WriteWholeNumber(2772, NoMatchExceptions.Length);
				for (int i = 0; i < NoMatchExceptions.Length; i++)
				{
					((IFixEncoder)NoMatchExceptions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMatchExceptions") is IMessageView viewNoMatchExceptions)
			{
				var count = viewNoMatchExceptions.GroupCount();
				NoMatchExceptions = new ConfirmationNoMatchExceptions[count];
				for (int i = 0; i < count; i++)
				{
					NoMatchExceptions[i] = new();
					((IFixParser)NoMatchExceptions[i]).Parse(viewNoMatchExceptions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMatchExceptions":
					value = NoMatchExceptions;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMatchExceptions = null;
		}
	}
}
