using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class QuotQualGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 735, Offset = 0, Required = false)]
		public NoQuoteQualifiers[]? NoQuoteQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteQualifiers is not null && NoQuoteQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(735, NoQuoteQualifiers.Length);
				for (int i = 0; i < NoQuoteQualifiers.Length; i++)
				{
					((IFixEncoder)NoQuoteQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoQuoteQualifiers") is IMessageView viewNoQuoteQualifiers)
			{
				var count = viewNoQuoteQualifiers.GroupCount();
				NoQuoteQualifiers = new NoQuoteQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteQualifiers[i] = new();
					((IFixParser)NoQuoteQualifiers[i]).Parse(viewNoQuoteQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoQuoteQualifiers":
					value = NoQuoteQualifiers;
					break;
				default: return false;
			}
			return true;
		}
	}
}
