using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProtectionTermEventNewsSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42090, Offset = 0, Required = false)]
		public NoUnderlyingProtectionTermEventNewsSources[]? NoUnderlyingProtectionTermEventNewsSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProtectionTermEventNewsSources is not null && NoUnderlyingProtectionTermEventNewsSources.Length != 0)
			{
				writer.WriteWholeNumber(42090, NoUnderlyingProtectionTermEventNewsSources.Length);
				for (int i = 0; i < NoUnderlyingProtectionTermEventNewsSources.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProtectionTermEventNewsSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProtectionTermEventNewsSources") is IMessageView viewNoUnderlyingProtectionTermEventNewsSources)
			{
				var count = viewNoUnderlyingProtectionTermEventNewsSources.GroupCount();
				NoUnderlyingProtectionTermEventNewsSources = new NoUnderlyingProtectionTermEventNewsSources[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProtectionTermEventNewsSources[i] = new();
					((IFixParser)NoUnderlyingProtectionTermEventNewsSources[i]).Parse(viewNoUnderlyingProtectionTermEventNewsSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProtectionTermEventNewsSources":
					value = NoUnderlyingProtectionTermEventNewsSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProtectionTermEventNewsSources = null;
		}
	}
}
