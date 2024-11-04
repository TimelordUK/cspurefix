using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MiscFeesSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2633, Offset = 0, Required = false)]
		public NoMiscFeeSubTypes[]? NoMiscFeeSubTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMiscFeeSubTypes is not null && NoMiscFeeSubTypes.Length != 0)
			{
				writer.WriteWholeNumber(2633, NoMiscFeeSubTypes.Length);
				for (int i = 0; i < NoMiscFeeSubTypes.Length; i++)
				{
					((IFixEncoder)NoMiscFeeSubTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMiscFeeSubTypes") is IMessageView viewNoMiscFeeSubTypes)
			{
				var count = viewNoMiscFeeSubTypes.GroupCount();
				NoMiscFeeSubTypes = new NoMiscFeeSubTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoMiscFeeSubTypes[i] = new();
					((IFixParser)NoMiscFeeSubTypes[i]).Parse(viewNoMiscFeeSubTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMiscFeeSubTypes":
					value = NoMiscFeeSubTypes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMiscFeeSubTypes = null;
		}
	}
}
