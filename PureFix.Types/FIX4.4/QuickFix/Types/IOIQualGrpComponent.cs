using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class IOIQualGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 199, Offset = 0, Required = false)]
		public IOINoIOIQualifiers[]? NoIOIQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoIOIQualifiers is not null && NoIOIQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(199, NoIOIQualifiers.Length);
				for (int i = 0; i < NoIOIQualifiers.Length; i++)
				{
					((IFixEncoder)NoIOIQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoIOIQualifiers") is IMessageView viewNoIOIQualifiers)
			{
				var count = viewNoIOIQualifiers.GroupCount();
				NoIOIQualifiers = new IOINoIOIQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoIOIQualifiers[i] = new();
					((IFixParser)NoIOIQualifiers[i]).Parse(viewNoIOIQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoIOIQualifiers":
					value = NoIOIQualifiers;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoIOIQualifiers = null;
		}
	}
}
