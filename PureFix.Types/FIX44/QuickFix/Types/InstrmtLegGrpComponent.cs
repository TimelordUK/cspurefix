using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtLegGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 555, Offset = 0, Required = false)]
		public AdvertisementInstrmtLegGrpNoLegs[]? NoLegs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegs is not null && NoLegs.Length != 0)
			{
				writer.WriteWholeNumber(555, NoLegs.Length);
				for (int i = 0; i < NoLegs.Length; i++)
				{
					((IFixEncoder)NoLegs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegs") is IMessageView viewNoLegs)
			{
				var count = viewNoLegs.GroupCount();
				NoLegs = new AdvertisementInstrmtLegGrpNoLegs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegs[i] = new();
					((IFixParser)NoLegs[i]).Parse(viewNoLegs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegs":
					value = NoLegs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegs = null;
		}
	}
}
