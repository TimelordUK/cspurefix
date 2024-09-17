using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AssetAttributeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2304, Offset = 0, Required = false)]
		public NoAssetAttributes[]? NoAssetAttributes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAssetAttributes is not null && NoAssetAttributes.Length != 0)
			{
				writer.WriteWholeNumber(2304, NoAssetAttributes.Length);
				for (int i = 0; i < NoAssetAttributes.Length; i++)
				{
					((IFixEncoder)NoAssetAttributes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAssetAttributes") is IMessageView viewNoAssetAttributes)
			{
				var count = viewNoAssetAttributes.GroupCount();
				NoAssetAttributes = new NoAssetAttributes[count];
				for (int i = 0; i < count; i++)
				{
					NoAssetAttributes[i] = new();
					((IFixParser)NoAssetAttributes[i]).Parse(viewNoAssetAttributes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAssetAttributes":
					value = NoAssetAttributes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
