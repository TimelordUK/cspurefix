using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TrdCollGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 897, Offset = 0, Required = false)]
		public NoTrades[]? NoTrades {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrades is not null && NoTrades.Length != 0)
			{
				writer.WriteWholeNumber(897, NoTrades.Length);
				for (int i = 0; i < NoTrades.Length; i++)
				{
					((IFixEncoder)NoTrades[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTrades") is IMessageView viewNoTrades)
			{
				var count = viewNoTrades.GroupCount();
				NoTrades = new NoTrades[count];
				for (int i = 0; i < count; i++)
				{
					NoTrades[i] = new();
					((IFixParser)NoTrades[i]).Parse(viewNoTrades.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTrades":
					value = NoTrades;
					break;
				default: return false;
			}
			return true;
		}
	}
}
