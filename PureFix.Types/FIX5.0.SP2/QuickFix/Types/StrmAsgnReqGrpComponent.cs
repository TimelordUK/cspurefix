using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StrmAsgnReqGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1499, Offset = 0, Required = false)]
		public NoAsgnReqs[]? NoAsgnReqs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAsgnReqs is not null && NoAsgnReqs.Length != 0)
			{
				writer.WriteWholeNumber(1499, NoAsgnReqs.Length);
				for (int i = 0; i < NoAsgnReqs.Length; i++)
				{
					((IFixEncoder)NoAsgnReqs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAsgnReqs") is IMessageView viewNoAsgnReqs)
			{
				var count = viewNoAsgnReqs.GroupCount();
				NoAsgnReqs = new NoAsgnReqs[count];
				for (int i = 0; i < count; i++)
				{
					NoAsgnReqs[i] = new();
					((IFixParser)NoAsgnReqs[i]).Parse(viewNoAsgnReqs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAsgnReqs":
					value = NoAsgnReqs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
