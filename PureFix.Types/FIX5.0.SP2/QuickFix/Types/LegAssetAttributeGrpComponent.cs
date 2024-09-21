using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegAssetAttributeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2308, Offset = 0, Required = false)]
		public NoLegAssetAttributes[]? NoLegAssetAttributes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegAssetAttributes is not null && NoLegAssetAttributes.Length != 0)
			{
				writer.WriteWholeNumber(2308, NoLegAssetAttributes.Length);
				for (int i = 0; i < NoLegAssetAttributes.Length; i++)
				{
					((IFixEncoder)NoLegAssetAttributes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegAssetAttributes") is IMessageView viewNoLegAssetAttributes)
			{
				var count = viewNoLegAssetAttributes.GroupCount();
				NoLegAssetAttributes = new NoLegAssetAttributes[count];
				for (int i = 0; i < count; i++)
				{
					NoLegAssetAttributes[i] = new();
					((IFixParser)NoLegAssetAttributes[i]).Parse(viewNoLegAssetAttributes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegAssetAttributes":
					value = NoLegAssetAttributes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegAssetAttributes = null;
		}
	}
}
