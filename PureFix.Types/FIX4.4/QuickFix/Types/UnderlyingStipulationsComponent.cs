using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UnderlyingStipulationsComponent : IFixComponent
	{
		[Group(NoOfTag = 887, Offset = 0, Required = false)]
		public NoUnderlyingStips[]? NoUnderlyingStips {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStips is not null && NoUnderlyingStips.Length != 0)
			{
				writer.WriteWholeNumber(887, NoUnderlyingStips.Length);
				for (int i = 0; i < NoUnderlyingStips.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStips[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStips") is IMessageView viewNoUnderlyingStips)
			{
				var count = viewNoUnderlyingStips.GroupCount();
				NoUnderlyingStips = new NoUnderlyingStips[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStips[i] = new();
					((IFixParser)NoUnderlyingStips[i]).Parse(viewNoUnderlyingStips.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStips":
					value = NoUnderlyingStips;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStips = null;
		}
	}
}
