using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamAssetAttributeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41800, Offset = 0, Required = false)]
		public NoUnderlyingStreamAssetAttributes[]? NoUnderlyingStreamAssetAttributes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamAssetAttributes is not null && NoUnderlyingStreamAssetAttributes.Length != 0)
			{
				writer.WriteWholeNumber(41800, NoUnderlyingStreamAssetAttributes.Length);
				for (int i = 0; i < NoUnderlyingStreamAssetAttributes.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamAssetAttributes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamAssetAttributes") is IMessageView viewNoUnderlyingStreamAssetAttributes)
			{
				var count = viewNoUnderlyingStreamAssetAttributes.GroupCount();
				NoUnderlyingStreamAssetAttributes = new NoUnderlyingStreamAssetAttributes[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamAssetAttributes[i] = new();
					((IFixParser)NoUnderlyingStreamAssetAttributes[i]).Parse(viewNoUnderlyingStreamAssetAttributes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamAssetAttributes":
					value = NoUnderlyingStreamAssetAttributes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamAssetAttributes = null;
		}
	}
}
