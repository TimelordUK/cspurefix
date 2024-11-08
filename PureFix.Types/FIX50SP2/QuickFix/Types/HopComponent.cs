using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class HopComponent : IFixComponent
	{
		[Group(NoOfTag = 627, Offset = 0, Required = false)]
		public LogoutNoHops[]? NoHops {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoHops is not null && NoHops.Length != 0)
			{
				writer.WriteWholeNumber(627, NoHops.Length);
				for (int i = 0; i < NoHops.Length; i++)
				{
					((IFixEncoder)NoHops[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoHops") is IMessageView viewNoHops)
			{
				var count = viewNoHops.GroupCount();
				NoHops = new LogoutNoHops[count];
				for (int i = 0; i < count; i++)
				{
					NoHops[i] = new();
					((IFixParser)NoHops[i]).Parse(viewNoHops.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoHops":
					value = NoHops;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoHops = null;
		}
	}
}
