using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecondaryAssetGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1976, Offset = 0, Required = false)]
		public NoSecondaryAssetClasses[]? NoSecondaryAssetClasses {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSecondaryAssetClasses is not null && NoSecondaryAssetClasses.Length != 0)
			{
				writer.WriteWholeNumber(1976, NoSecondaryAssetClasses.Length);
				for (int i = 0; i < NoSecondaryAssetClasses.Length; i++)
				{
					((IFixEncoder)NoSecondaryAssetClasses[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSecondaryAssetClasses") is IMessageView viewNoSecondaryAssetClasses)
			{
				var count = viewNoSecondaryAssetClasses.GroupCount();
				NoSecondaryAssetClasses = new NoSecondaryAssetClasses[count];
				for (int i = 0; i < count; i++)
				{
					NoSecondaryAssetClasses[i] = new();
					((IFixParser)NoSecondaryAssetClasses[i]).Parse(viewNoSecondaryAssetClasses.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSecondaryAssetClasses":
					value = NoSecondaryAssetClasses;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSecondaryAssetClasses = null;
		}
	}
}
