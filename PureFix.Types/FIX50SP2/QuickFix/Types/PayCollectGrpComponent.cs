using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PayCollectGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1707, Offset = 0, Required = false)]
		public NoPayCollects[]? NoPayCollects {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPayCollects is not null && NoPayCollects.Length != 0)
			{
				writer.WriteWholeNumber(1707, NoPayCollects.Length);
				for (int i = 0; i < NoPayCollects.Length; i++)
				{
					((IFixEncoder)NoPayCollects[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPayCollects") is IMessageView viewNoPayCollects)
			{
				var count = viewNoPayCollects.GroupCount();
				NoPayCollects = new NoPayCollects[count];
				for (int i = 0; i < count; i++)
				{
					NoPayCollects[i] = new();
					((IFixParser)NoPayCollects[i]).Parse(viewNoPayCollects.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPayCollects":
					value = NoPayCollects;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPayCollects = null;
		}
	}
}
