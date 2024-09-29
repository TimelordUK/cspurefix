using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDividendFXTriggerDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42853, Offset = 0, Required = false)]
		public NoUnderlyingDividendFXTriggerDateBusinessCenters[]? NoUnderlyingDividendFXTriggerDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDividendFXTriggerDateBusinessCenters is not null && NoUnderlyingDividendFXTriggerDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42853, NoUnderlyingDividendFXTriggerDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingDividendFXTriggerDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDividendFXTriggerDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDividendFXTriggerDateBusinessCenters") is IMessageView viewNoUnderlyingDividendFXTriggerDateBusinessCenters)
			{
				var count = viewNoUnderlyingDividendFXTriggerDateBusinessCenters.GroupCount();
				NoUnderlyingDividendFXTriggerDateBusinessCenters = new NoUnderlyingDividendFXTriggerDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDividendFXTriggerDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingDividendFXTriggerDateBusinessCenters[i]).Parse(viewNoUnderlyingDividendFXTriggerDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDividendFXTriggerDateBusinessCenters":
					value = NoUnderlyingDividendFXTriggerDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingDividendFXTriggerDateBusinessCenters = null;
		}
	}
}
