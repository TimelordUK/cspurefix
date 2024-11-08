using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProtectionTermEventNewsSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40951, Offset = 0, Required = false)]
		public IOINoProtectionTermEventNewsSources[]? NoProtectionTermEventNewsSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProtectionTermEventNewsSources is not null && NoProtectionTermEventNewsSources.Length != 0)
			{
				writer.WriteWholeNumber(40951, NoProtectionTermEventNewsSources.Length);
				for (int i = 0; i < NoProtectionTermEventNewsSources.Length; i++)
				{
					((IFixEncoder)NoProtectionTermEventNewsSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProtectionTermEventNewsSources") is IMessageView viewNoProtectionTermEventNewsSources)
			{
				var count = viewNoProtectionTermEventNewsSources.GroupCount();
				NoProtectionTermEventNewsSources = new IOINoProtectionTermEventNewsSources[count];
				for (int i = 0; i < count; i++)
				{
					NoProtectionTermEventNewsSources[i] = new();
					((IFixParser)NoProtectionTermEventNewsSources[i]).Parse(viewNoProtectionTermEventNewsSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProtectionTermEventNewsSources":
					value = NoProtectionTermEventNewsSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProtectionTermEventNewsSources = null;
		}
	}
}
