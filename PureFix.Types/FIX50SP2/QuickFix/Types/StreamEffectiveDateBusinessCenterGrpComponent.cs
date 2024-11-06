using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamEffectiveDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40960, Offset = 0, Required = false)]
		public IOINoStreamEffectiveDateBusinessCenters[]? NoStreamEffectiveDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamEffectiveDateBusinessCenters is not null && NoStreamEffectiveDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40960, NoStreamEffectiveDateBusinessCenters.Length);
				for (int i = 0; i < NoStreamEffectiveDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoStreamEffectiveDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamEffectiveDateBusinessCenters") is IMessageView viewNoStreamEffectiveDateBusinessCenters)
			{
				var count = viewNoStreamEffectiveDateBusinessCenters.GroupCount();
				NoStreamEffectiveDateBusinessCenters = new IOINoStreamEffectiveDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamEffectiveDateBusinessCenters[i] = new();
					((IFixParser)NoStreamEffectiveDateBusinessCenters[i]).Parse(viewNoStreamEffectiveDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamEffectiveDateBusinessCenters":
					value = NoStreamEffectiveDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStreamEffectiveDateBusinessCenters = null;
		}
	}
}
