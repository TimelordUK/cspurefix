using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ValueChecksGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1868, Offset = 0, Required = false)]
		public NoValueChecks[]? NoValueChecks {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoValueChecks is not null && NoValueChecks.Length != 0)
			{
				writer.WriteWholeNumber(1868, NoValueChecks.Length);
				for (int i = 0; i < NoValueChecks.Length; i++)
				{
					((IFixEncoder)NoValueChecks[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoValueChecks") is IMessageView viewNoValueChecks)
			{
				var count = viewNoValueChecks.GroupCount();
				NoValueChecks = new NoValueChecks[count];
				for (int i = 0; i < count; i++)
				{
					NoValueChecks[i] = new();
					((IFixParser)NoValueChecks[i]).Parse(viewNoValueChecks.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoValueChecks":
					value = NoValueChecks;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoValueChecks = null;
		}
	}
}
