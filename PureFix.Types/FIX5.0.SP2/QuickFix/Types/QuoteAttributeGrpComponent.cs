using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class QuoteAttributeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2706, Offset = 0, Required = false)]
		public NoQuoteAttributes[]? NoQuoteAttributes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteAttributes is not null && NoQuoteAttributes.Length != 0)
			{
				writer.WriteWholeNumber(2706, NoQuoteAttributes.Length);
				for (int i = 0; i < NoQuoteAttributes.Length; i++)
				{
					((IFixEncoder)NoQuoteAttributes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoQuoteAttributes") is IMessageView viewNoQuoteAttributes)
			{
				var count = viewNoQuoteAttributes.GroupCount();
				NoQuoteAttributes = new NoQuoteAttributes[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteAttributes[i] = new();
					((IFixParser)NoQuoteAttributes[i]).Parse(viewNoQuoteAttributes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoQuoteAttributes":
					value = NoQuoteAttributes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
