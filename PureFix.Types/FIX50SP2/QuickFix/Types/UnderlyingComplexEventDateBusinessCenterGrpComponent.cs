using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41737, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventDateBusinessCenters[]? NoUnderlyingComplexEventDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventDateBusinessCenters is not null && NoUnderlyingComplexEventDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41737, NoUnderlyingComplexEventDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingComplexEventDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventDateBusinessCenters") is IMessageView viewNoUnderlyingComplexEventDateBusinessCenters)
			{
				var count = viewNoUnderlyingComplexEventDateBusinessCenters.GroupCount();
				NoUnderlyingComplexEventDateBusinessCenters = new NoUnderlyingComplexEventDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingComplexEventDateBusinessCenters[i]).Parse(viewNoUnderlyingComplexEventDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventDateBusinessCenters":
					value = NoUnderlyingComplexEventDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventDateBusinessCenters = null;
		}
	}
}
