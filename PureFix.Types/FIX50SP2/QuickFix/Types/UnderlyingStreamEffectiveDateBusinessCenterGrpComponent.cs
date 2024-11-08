using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamEffectiveDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40975, Offset = 0, Required = false)]
		public IOINoUnderlyingStreamEffectiveDateBusinessCenters[]? NoUnderlyingStreamEffectiveDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamEffectiveDateBusinessCenters is not null && NoUnderlyingStreamEffectiveDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40975, NoUnderlyingStreamEffectiveDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingStreamEffectiveDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamEffectiveDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamEffectiveDateBusinessCenters") is IMessageView viewNoUnderlyingStreamEffectiveDateBusinessCenters)
			{
				var count = viewNoUnderlyingStreamEffectiveDateBusinessCenters.GroupCount();
				NoUnderlyingStreamEffectiveDateBusinessCenters = new IOINoUnderlyingStreamEffectiveDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamEffectiveDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingStreamEffectiveDateBusinessCenters[i]).Parse(viewNoUnderlyingStreamEffectiveDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamEffectiveDateBusinessCenters":
					value = NoUnderlyingStreamEffectiveDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamEffectiveDateBusinessCenters = null;
		}
	}
}
