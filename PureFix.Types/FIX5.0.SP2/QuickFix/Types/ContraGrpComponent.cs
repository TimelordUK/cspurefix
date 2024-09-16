using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ContraGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 382, Offset = 0, Required = false)]
		public NoContraBrokers[]? NoContraBrokers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoContraBrokers is not null && NoContraBrokers.Length != 0)
			{
				writer.WriteWholeNumber(382, NoContraBrokers.Length);
				for (int i = 0; i < NoContraBrokers.Length; i++)
				{
					((IFixEncoder)NoContraBrokers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoContraBrokers") is IMessageView viewNoContraBrokers)
			{
				var count = viewNoContraBrokers.GroupCount();
				NoContraBrokers = new NoContraBrokers[count];
				for (int i = 0; i < count; i++)
				{
					NoContraBrokers[i] = new();
					((IFixParser)NoContraBrokers[i]).Parse(viewNoContraBrokers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoContraBrokers":
					value = NoContraBrokers;
					break;
				default: return false;
			}
			return true;
		}
	}
}
