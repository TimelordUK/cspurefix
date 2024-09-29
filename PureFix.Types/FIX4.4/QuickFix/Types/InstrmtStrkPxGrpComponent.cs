using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtStrkPxGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 428, Offset = 0, Required = true)]
		public NoStrikes[]? NoStrikes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoStrikes is not null && FixValidator.IsValid(NoStrikes, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStrikes is not null && NoStrikes.Length != 0)
			{
				writer.WriteWholeNumber(428, NoStrikes.Length);
				for (int i = 0; i < NoStrikes.Length; i++)
				{
					((IFixEncoder)NoStrikes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStrikes") is IMessageView viewNoStrikes)
			{
				var count = viewNoStrikes.GroupCount();
				NoStrikes = new NoStrikes[count];
				for (int i = 0; i < count; i++)
				{
					NoStrikes[i] = new();
					((IFixParser)NoStrikes[i]).Parse(viewNoStrikes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStrikes":
					value = NoStrikes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStrikes = null;
		}
	}
}
