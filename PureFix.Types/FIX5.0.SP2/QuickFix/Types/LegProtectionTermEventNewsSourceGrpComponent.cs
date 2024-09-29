using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProtectionTermEventNewsSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41614, Offset = 0, Required = false)]
		public NoLegProtectionTermEventNewsSources[]? NoLegProtectionTermEventNewsSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProtectionTermEventNewsSources is not null && NoLegProtectionTermEventNewsSources.Length != 0)
			{
				writer.WriteWholeNumber(41614, NoLegProtectionTermEventNewsSources.Length);
				for (int i = 0; i < NoLegProtectionTermEventNewsSources.Length; i++)
				{
					((IFixEncoder)NoLegProtectionTermEventNewsSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProtectionTermEventNewsSources") is IMessageView viewNoLegProtectionTermEventNewsSources)
			{
				var count = viewNoLegProtectionTermEventNewsSources.GroupCount();
				NoLegProtectionTermEventNewsSources = new NoLegProtectionTermEventNewsSources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProtectionTermEventNewsSources[i] = new();
					((IFixParser)NoLegProtectionTermEventNewsSources[i]).Parse(viewNoLegProtectionTermEventNewsSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProtectionTermEventNewsSources":
					value = NoLegProtectionTermEventNewsSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProtectionTermEventNewsSources = null;
		}
	}
}
