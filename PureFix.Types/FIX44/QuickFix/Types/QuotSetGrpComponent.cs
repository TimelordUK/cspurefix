using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotSetGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 296, Offset = 0, Required = true)]
		public MassQuoteNoQuoteSets[]? NoQuoteSets {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoQuoteSets is not null && FixValidator.IsValid(NoQuoteSets, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteSets is not null && NoQuoteSets.Length != 0)
			{
				writer.WriteWholeNumber(296, NoQuoteSets.Length);
				for (int i = 0; i < NoQuoteSets.Length; i++)
				{
					((IFixEncoder)NoQuoteSets[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoQuoteSets") is IMessageView viewNoQuoteSets)
			{
				var count = viewNoQuoteSets.GroupCount();
				NoQuoteSets = new MassQuoteNoQuoteSets[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteSets[i] = new();
					((IFixParser)NoQuoteSets[i]).Parse(viewNoQuoteSets.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoQuoteSets":
					value = NoQuoteSets;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoQuoteSets = null;
		}
	}
}
