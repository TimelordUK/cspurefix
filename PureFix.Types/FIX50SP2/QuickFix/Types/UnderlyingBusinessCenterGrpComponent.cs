using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40962, Offset = 0, Required = false)]
		public IOINoUnderlyingBusinessCenters[]? NoUnderlyingBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingBusinessCenters is not null && NoUnderlyingBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40962, NoUnderlyingBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingBusinessCenters") is IMessageView viewNoUnderlyingBusinessCenters)
			{
				var count = viewNoUnderlyingBusinessCenters.GroupCount();
				NoUnderlyingBusinessCenters = new IOINoUnderlyingBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingBusinessCenters[i]).Parse(viewNoUnderlyingBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingBusinessCenters":
					value = NoUnderlyingBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingBusinessCenters = null;
		}
	}
}
