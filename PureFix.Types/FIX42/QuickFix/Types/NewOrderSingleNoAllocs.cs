using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class NewOrderSingleNoAllocs : IFixGroup
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount {get; set;}
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 1, Required = false)]
		public double? AllocShares {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (AllocShares is not null) writer.WriteNumber(80, AllocShares.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AllocAccount = view.GetString(79);
			AllocShares = view.GetDouble(80);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AllocAccount":
					value = AllocAccount;
					break;
				case "AllocShares":
					value = AllocShares;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			AllocAccount = null;
			AllocShares = null;
		}
	}
}
