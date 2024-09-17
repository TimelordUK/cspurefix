using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TransactionAttributeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2871, Offset = 0, Required = false)]
		public NoTransactionAttributes[]? NoTransactionAttributes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTransactionAttributes is not null && NoTransactionAttributes.Length != 0)
			{
				writer.WriteWholeNumber(2871, NoTransactionAttributes.Length);
				for (int i = 0; i < NoTransactionAttributes.Length; i++)
				{
					((IFixEncoder)NoTransactionAttributes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTransactionAttributes") is IMessageView viewNoTransactionAttributes)
			{
				var count = viewNoTransactionAttributes.GroupCount();
				NoTransactionAttributes = new NoTransactionAttributes[count];
				for (int i = 0; i < count; i++)
				{
					NoTransactionAttributes[i] = new();
					((IFixParser)NoTransactionAttributes[i]).Parse(viewNoTransactionAttributes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTransactionAttributes":
					value = NoTransactionAttributes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
