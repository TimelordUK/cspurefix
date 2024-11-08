using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecCollGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 124, Offset = 0, Required = false)]
		public CollateralRequestNoExecs[]? NoExecs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoExecs is not null && NoExecs.Length != 0)
			{
				writer.WriteWholeNumber(124, NoExecs.Length);
				for (int i = 0; i < NoExecs.Length; i++)
				{
					((IFixEncoder)NoExecs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoExecs") is IMessageView viewNoExecs)
			{
				var count = viewNoExecs.GroupCount();
				NoExecs = new CollateralRequestNoExecs[count];
				for (int i = 0; i < count; i++)
				{
					NoExecs[i] = new();
					((IFixParser)NoExecs[i]).Parse(viewNoExecs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoExecs":
					value = NoExecs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoExecs = null;
		}
	}
}
