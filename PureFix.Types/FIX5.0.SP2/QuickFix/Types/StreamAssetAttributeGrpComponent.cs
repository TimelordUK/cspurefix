using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamAssetAttributeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41237, Offset = 0, Required = false)]
		public NoStreamAssetAttributes[]? NoStreamAssetAttributes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamAssetAttributes is not null && NoStreamAssetAttributes.Length != 0)
			{
				writer.WriteWholeNumber(41237, NoStreamAssetAttributes.Length);
				for (int i = 0; i < NoStreamAssetAttributes.Length; i++)
				{
					((IFixEncoder)NoStreamAssetAttributes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamAssetAttributes") is IMessageView viewNoStreamAssetAttributes)
			{
				var count = viewNoStreamAssetAttributes.GroupCount();
				NoStreamAssetAttributes = new NoStreamAssetAttributes[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamAssetAttributes[i] = new();
					((IFixParser)NoStreamAssetAttributes[i]).Parse(viewNoStreamAssetAttributes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamAssetAttributes":
					value = NoStreamAssetAttributes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
